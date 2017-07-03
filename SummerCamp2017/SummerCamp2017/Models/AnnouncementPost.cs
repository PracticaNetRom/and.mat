using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerCamp2017.Models
{
    public class AnnouncementPost
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Title { get; set; }
                   
    }
}