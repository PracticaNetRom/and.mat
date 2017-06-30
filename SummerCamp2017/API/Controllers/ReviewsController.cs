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
        public HttpResponseMessage Get()
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                var data = ctx.Reviews.Select(c =>
               new
               {
                   Id = c.ReviewId
                }).ToList();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
        }
    }
}
