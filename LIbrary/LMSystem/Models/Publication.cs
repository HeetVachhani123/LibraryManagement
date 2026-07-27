using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public enum PublicationType
    {
        Newspaper = 0,
        Magazine = 1
    }

    [Table("Publications")]
    public class Publication
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required]
        public PublicationType Type { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
