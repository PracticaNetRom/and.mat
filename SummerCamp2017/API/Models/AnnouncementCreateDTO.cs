using System.ComponentModel.DataAnnotations;


namespace API.Models
{
    public class AnnouncementCreateDTO
    {
        [Required]
        [StringLength(16)]
        public string Phonenumber { get; set; }

        [Required]
        [StringLength(64)]
        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        public int CategoryId { get; set; }
    }
}