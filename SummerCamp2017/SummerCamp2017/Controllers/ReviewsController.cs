using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SummerCamp2017.Models;
using Plugin.RestClient;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SummerCamp2017.Controllers
{
    public class ReviewsController : Controller
    {

        
        // GET: Reviews
        public ActionResult Index()
        {
            return View();
        }
        public AnnouncementDetails GetAnnouncementById(int id)
        {
            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            var announcement = rc.GetByIdAsync(id);
            return announcement;
        }
        public List<Review> GetReviewsForAnnouncement(int id)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:10469/api/reviews/GetByAnnouncementId?announcementId=" + id;
            var reviewList = rc.GetAsync();
            return reviewList;
        }
        // POST: Review
        public System.Net.Http.HttpResponseMessage PostReview(Review rev)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:10469/api/reviews/";
            System.Net.Http.HttpResponseMessage response = rc.PostAsync(rev);
            return response;
        }
       

        public ActionResult List(int id)
        {
            ReviewsForAnnouncement reviewsForAnnouncement = new ReviewsForAnnouncement();
            reviewsForAnnouncement.CurrentAnnouncementId = id;
            List<Review> reviewsList = GetReviewsForAnnouncement(id);
            reviewsForAnnouncement.Reviews = reviewsList;

            return View(reviewsForAnnouncement);
        }

        public ActionResult CreateRev(int announcementId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRev(Review review)
        {
            AnnouncementDetails a = GetAnnouncementById(review.AnnouncementId);
            try
            {
                var rev = PostReview(review);
                var MailHelper = new MailHelper
                {
                    
                    Sender = "mateiasiandreea19@gmail.com", //email.Sender,
                    Recipient = a.Email,
                    RecipientCC = null,
                    Subject = "a new comment",
                    Body = "Someone added this comment: ' "+review.Comment+ "  ' to your announcement : " +a.Title
                };
                MailHelper.Send();

                //PostAnnouncement(model);
                
            }
            catch (Exception e)
            {
                ModelState.AddModelError("err", e);
                return View();
            }
            //return RedirectToAction("List");
            

            return RedirectToAction("Details", "Announcements", new { id = review.AnnouncementId });
        }



    }
}