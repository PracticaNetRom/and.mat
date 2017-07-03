using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SummerCamp2017.Models;
using Plugin.RestClient;

namespace SummerCamp2017.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: Reviews
        public ActionResult Index()
        {
            return View();
        }
        
        public List<Review> GetReviewsForAnnouncement(int id)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:10469/api/reviews/GetByAnnouncementId?announcementId=" + id;
            var reviewList = rc.GetAsync();
            return reviewList;
        }
        // POST: Review
        public bool PostReview(Review rev)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:10469/api/reviews/";
            bool response = rc.PostAsync(rev);
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

        public ActionResult Create(int announcementId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int announcementId, Review review)
        {
            review.AnnouncementId = announcementId;
            PostReview(review);

            return RedirectToAction("List", new { id = announcementId });
        }



    }
}