using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DataAccess
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }

        public string Phonenumber { get; set; }

        public string Email { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public bool Closed { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public Announcement()
        {
            Reviews = new List<Review>();
        }
    }
}