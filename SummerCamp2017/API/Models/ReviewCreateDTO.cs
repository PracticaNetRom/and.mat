using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ReviewCreateDTO
    {
        public int AnnouncementId { get; set; }

        public int? Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public string Username { get; set; }
    }
}

