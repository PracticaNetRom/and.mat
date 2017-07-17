using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.DataAccess;
using API.Models;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace SummerCamp.WebAPI.Controllers
{
    public class AnnouncementsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
               
                 var data = ctx.Announcements.Where(a => a.Confirmed).Select(a =>
                     new
                     {
                         AnnouncementId = a.AnnouncementId,
                         Closed = a.Closed,
                         Title = a.Title,
                         CategoryName = a.Category.Name,
                         PostDate = a.PostDate,
                         ExpirationDate = a.ExpirationDate
                     }).ToList();





                 HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                 return response;
            }

        }

        //[ResponseType(typeof(void))]
        //[HttpPut]
        //[Route("api/Announcements/PutAnnouncement")]

        //public IHttpActionResult PutAnnouncement(int id, [FromBody]Announcement a)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != a.AnnouncementId)
        //    {
        //        return BadRequest();
        //    }
        //    using (SummerCampDbContext ctx = new SummerCampDbContext())
        //    {
        //        Announcement foundAnnouncement = ctx.Announcements.Find(a.AnnouncementId);
        //        foundAnnouncement.CategoryId = a.CategoryId;
        //        foundAnnouncement.Title = a.Title;
        //        foundAnnouncement.Description = a.Description;
        //        foundAnnouncement.Email = a.Email;
        //        foundAnnouncement.Phonenumber = a.Phonenumber;
        //        foundAnnouncement.PostDate = DateTime.Now;
        //        foundAnnouncement.ExpirationDate = DateTime.Now.AddMonths(1);
        //        ctx.SaveChanges();

               
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement announcement = ctx.Announcements.Include(a => a.Category).FirstOrDefault(a => a.AnnouncementId == id);

                if (announcement == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var data = new { Id = announcement.AnnouncementId, CategoryName = announcement.Category.Name, CategoryId = announcement.Category.CategoryId, ExpirationDate = announcement.ExpirationDate, PostDate = announcement.PostDate, Phonenumber = announcement.Phonenumber, Title = announcement.Title, Closed = announcement.Closed, Description = announcement.Description, Email = announcement.Email, Confirmed = announcement.Confirmed };

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }


        [HttpPost]
        [Route("api/Announcements/Search/{CategoryId}")]

        public HttpResponseMessage Search([FromUri]int? CategoryId,[FromBody] Title model)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                //IEnumerable<Announcement> a = ctx.Announcements;//.FirstOrDefault(a => a.AnnouncementId == CategoryId);


                //if (CategoryId.HasValue)
                //{
                //    a = a.Where(ann => ann.CategoryId == CategoryId);
                //    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);

                //    //return response;
                //    //return Request.CreateResponse(HttpStatusCode.OK, data);
                //}
                //if (model.Text != null)
                //{
                //    a = a.Where(ann => ann.Title == model.Text);
                //}
                //a.ToList();

                IEnumerable<Announcement> ann = ctx.Announcements;
                if (CategoryId.HasValue)
                {
                    ann = ann.Where(a => a.CategoryId == CategoryId).ToList();
                }

                if(model.Text != null)
                {
                    ann = ann.Where(a => a.Title == model.Text).ToList();
                }
                //DateTime date1 = new DateTime(2009, 8, 1, 0, 0, 0);
                DateTime date = new DateTime(0001, 1, 1, 12, 0, 0);
                int result1 = DateTime.Compare(model.StartDate, date);
                int result2 = DateTime.Compare(model.EndDate, date);
                if (result1 != -1)
                {
                    ann = ann.Where(a => model.StartDate.Date < a.PostDate.Date).ToList();
                }
                if (result2 != -1)
                {
                    ann = ann.Where(a => a.ExpirationDate.Date > model.EndDate.Date).ToList();
                }
                if(ann == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    if(ann == ctx.Announcements)
                    {

                        var data = ctx.Announcements.Select(a =>
                                            new
                                            {
                                                AnnouncementId = a.AnnouncementId,
                                                CategoryId = a.CategoryId,
                                                Description = a.Description,
                                                Title = a.Title,
                                                CategoryName = a.Category.Name,
                                                StartDate = a.PostDate,
                                                EndDate = a.ExpirationDate
                                            }).ToList();





                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                        return response;
                    }
                    else
                    {
                        var data = ann.Select(a =>
                                                           new
                                                           {
                                                               AnnouncementId = a.AnnouncementId,
                                                               CategoryId = a.CategoryId,
                                                               Description = a.Description,
                                                               Title = a.Title,
                                                               CategoryName = a.Category.Name,
                                                               StartDate = a.PostDate,
                                                               EndDate = a.ExpirationDate
                                                           }).ToList();

                        return Request.CreateResponse(HttpStatusCode.OK, data);

                    }

                }
            }
        }


        [HttpPost]
        [Route("api/Announcements/NewAnnouncement")]
        public HttpResponseMessage NewAnnouncement([FromBody] AnnouncementCreateDTO announcement)
        {
            if (ModelState.IsValid)
            {
                Announcement entity = new Announcement
                {
                    CategoryId = announcement.CategoryId,
                    Description = announcement.Description,
                    Email = announcement.Email,
                    Phonenumber = announcement.Phonenumber,
                    Title = announcement.Title,
                    PostDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddMonths(1),
                    Confirmed = false
                };

                using (SummerCampDbContext ctx = new SummerCampDbContext())
                {
                    ctx.Announcements.Add(entity);
                    ctx.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created,  entity );
                //string uri = Url.Link("DefaultApi", new { Id = entity.AnnouncementId });
                //response.Headers.Location = new Uri(uri);
                return response;
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }
       
           

           

        [HttpPost]
        [Route("api/Announcements/CloseAnnouncement/{id}")]
        public HttpResponseMessage CloseAnnouncement([FromUri] int id, [FromBody] AnnouncementAuthDTO authInfo)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement announcement = ctx.Announcements.FirstOrDefault(a => a.AnnouncementId == id);

                if (string.Equals(announcement.Email, authInfo.Email, StringComparison.OrdinalIgnoreCase) && announcement.Confirmed)
                {
                    announcement.Closed = true;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }

        [HttpPost]
        [Route("api/Announcements/ExtendAnnouncement/{id}")]
        public HttpResponseMessage ExtendAnnouncement([FromUri] int id, [FromBody] AnnouncementAuthDTO authInfo)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement announcement = ctx.Announcements.FirstOrDefault(a => a.AnnouncementId == id);

                if (string.Equals(announcement.Email, authInfo.Email, StringComparison.OrdinalIgnoreCase))
                {
                    if (announcement.Closed && announcement.Confirmed)
                    {
                        return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
                    }

                    announcement.ExpirationDate = announcement.ExpirationDate.AddMonths(1);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
        [HttpPost]
        [Route("api/Announcements/ActivateAnnouncement/{id}")]
        public HttpResponseMessage ActivateAnnouncement([FromUri] int id, [FromBody] AnnouncementAuthDTO authInfo)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement announcement = ctx.Announcements.FirstOrDefault(a => a.AnnouncementId == id);

                if (string.Equals(announcement.Email, authInfo.Email, StringComparison.OrdinalIgnoreCase) && announcement.Confirmed == false)
                {
                    announcement.Confirmed = true;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                if (announcement.Confirmed)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);

                }

                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
}
