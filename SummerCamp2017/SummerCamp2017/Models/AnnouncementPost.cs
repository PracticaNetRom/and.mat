using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SummerCamp2017.Models
{
    public class AnnouncementPost
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]
        public string Email { get; set; }
        [Required]
        public string Phonenumber { get; set; }
        [Required]
        public string Title { get; set; }

                   
    }
}