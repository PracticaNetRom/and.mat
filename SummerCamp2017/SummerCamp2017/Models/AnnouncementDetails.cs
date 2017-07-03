using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerCamp2017.Models
{
    public class AnnouncementDetails
    {
        public int Id { get; set; }

        public string Phonenumber { get; set; }

        public string Email { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public bool Closed { get; set; }

        public int CategoryId { get; set; }

        public  string CategoryName { get; set; }

        //public IEnumerable<Review> Reviews { get; set; }
    }
}