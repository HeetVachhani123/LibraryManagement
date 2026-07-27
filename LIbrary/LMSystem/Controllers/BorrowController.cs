using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMSystem.Models;

namespace LMSystem.Controllers
{
    [Authorize]
    public class BorrowController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Borrow(int? bookId)
        {
            if (bookId == null) return NotFound();

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            if (!book.IsAvailable)
            {
                return View("NotAvailable", book);
            }

            var borrowRecord = new BorrowRecord
            {
                BookID = book.ID,
                BorrowDate = DateTime.Now
            };

            ViewBag.BookTitle = book.Title;
            return View(borrowRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(BorrowRecord borrowRecord)
        {
            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(borrowRecord.BookID);
                if (book != null && book.IsAvailable)
                {
                    book.IsAvailable = false;
                    _context.Update(book);
                    
                    _context.BorrowRecords.Add(borrowRecord);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction("Index", "Books");
                }
            }
            
            var b = await _context.Books.FindAsync(borrowRecord.BookID);
            ViewBag.BookTitle = b?.Title;
            return View(borrowRecord);
        }

        public async Task<IActionResult> Return(int? bookId)
        {
            if (bookId == null) return NotFound();

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            if (book.IsAvailable)
            {
                return View("AlreadyReturned", book);
            }

            var borrowRecord = await _context.BorrowRecords
                .Where(br => br.BookID == bookId && br.ReturnDate == null)
                .OrderByDescending(br => br.BorrowDate)
                .FirstOrDefaultAsync();

            if (borrowRecord == null)
            {
                return View("AlreadyReturned", book);
            }

            ViewBag.BookTitle = book.Title;
            return View(borrowRecord);
        }

        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord != null && borrowRecord.ReturnDate == null)
            {
                borrowRecord.ReturnDate = DateTime.Now;
                _context.Update(borrowRecord);

                var book = await _context.Books.FindAsync(borrowRecord.BookID);
                if (book != null)
                {
                    book.IsAvailable = true;
                    _context.Update(book);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Books");
        }
    }
}
