using Microsoft.AspNetCore.Mvc;
using LMSystem.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get 3 available books for featured section
            var featuredBooks = await _context.Books
                .Where(b => b.IsAvailable)
                .Take(3)
                .ToListAsync();

            return View(featuredBooks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
