using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    [Table("Books13")]
    public class Book
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        public string ISBN { get; set; }
        
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }
        
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Cover Image URL")]
        public string? ImageUrl { get; set; }
        
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
