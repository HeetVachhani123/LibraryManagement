using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    [Table("BorrowRecords13")]
    public class BorrowRecord
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }

        [Required]
        [Display(Name = "Borrower Name")]
        public string BorrowerName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Borrower Email")]
        public string BorrowerEmail { get; set; }

        public string Phone { get; set; }

        [Required]
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
    }
}
