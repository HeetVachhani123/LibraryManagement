using System.ComponentModel.DataAnnotations;

namespace LMSystem.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string Phone { get; set; }
    }
}
