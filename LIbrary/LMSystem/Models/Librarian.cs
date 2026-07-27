using System.ComponentModel.DataAnnotations;

namespace LMSystem.Models
{
    public class Librarian
    {
        public int LibrarianID { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Age { get; set; }
        
        public string Phone { get; set; }
    }
}
