using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SummerCamp2017.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
   
        public string Comment { get; set; }
        [Required]
        public string Username { get; set; }
        public int AnnouncementId { get; set; }
    }
}