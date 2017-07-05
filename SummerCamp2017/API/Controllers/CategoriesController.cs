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
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                var data = ctx.Categories.Select(c =>
                        new
                        {
                            CategoryId = c.CategoryId,
                            Name = c.Name
                        }).ToList();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
        }
    }
}
