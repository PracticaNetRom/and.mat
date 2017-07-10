using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ReviewCreateDTO
    {
        public int AnnouncementId { get; set; }
        

        public int? Rating { get; set; }
        public DateTime Postdate { get; set; }

        public string Comment { get; set; }

        [Required]
        public string Username { get; set; }
    }
}

