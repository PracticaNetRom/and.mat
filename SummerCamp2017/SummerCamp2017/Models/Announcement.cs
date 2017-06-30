using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerCamp2017.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public bool Closed { get; set; }
        public string CategoryName { get; set; }
    }
}