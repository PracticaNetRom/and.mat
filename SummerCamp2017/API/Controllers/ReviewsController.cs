using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.DataAccess;
using API.Models;

namespace API.Controllers
{
    public class ReviewsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetByAnnouncementId(int announcementId)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                var data = ctx.Reviews.Where(r => r.AnnouncementId == announcementId).Select(r =>
                  new
                  {
                      Rating = r.Rating,
                      Comment = r.Comment,
                      Username = r.Username
                  }).ToList();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage NewReview([FromBody] ReviewCreateDTO review)
        {
            if (string.IsNullOrEmpty(review.Comment) && !review.Rating.HasValue)
            {
                ModelState.AddModelError("", "Rating or comment required");
            }

            if (ModelState.IsValid)
            {
                Review entity = new Review
                {
                    AnnouncementId = review.AnnouncementId,
                    Comment = review.Comment,
                    Username = review.Username,
                    Rating = review.Rating
                };

                using (SummerCampDbContext ctx = new SummerCampDbContext())
                {
                    ctx.Reviews.Add(entity);
                    ctx.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, review);
                return response;
            }

            HttpError error = new HttpError(ModelState, false);

            return Request.CreateResponse(HttpStatusCode.BadRequest, error);
        }

    }
}
