using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerCamp2017.Models
{
    public class ReviewsForAnnouncement
    {
        public int CurrentAnnouncementId { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}