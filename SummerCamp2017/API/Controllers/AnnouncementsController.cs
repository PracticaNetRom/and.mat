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
               // List<Announcement> an = ctx.Announcement.ToList();
                /*intermediar obj = new intermediar();
                List<intermediar> list = new List<intermediar>();
                foreach (var a in ctx.Announcements)
                {
                    obj.AnnouncementId = a.AnnouncementId;
                    obj.Closed = a.Closed;
                    obj.Title = a.Title;
                    list.Add(obj);
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, list);
                return response;*/
                 var data = ctx.Announcements.Select(a =>
                     new
                     {
                         AnnouncementId = a.AnnouncementId,
                         Closed = a.Closed,
                         Title = a.Title,
                         CategoryName = a.Category.Name
                     }).ToList();





                 HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                 return response;
            }
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Announcements/PutAnnouncement")]

        public IHttpActionResult PutAnnouncement(int id, [FromBody]Announcement a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != a.AnnouncementId)
            {
                return BadRequest();
            }
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement foundAnnouncement = ctx.Announcements.Find(a.AnnouncementId);
                foundAnnouncement.CategoryId = a.CategoryId;
                foundAnnouncement.Title = a.Title;
                foundAnnouncement.Description = a.Description;
                foundAnnouncement.Email = a.Email;
                foundAnnouncement.Phonenumber = a.Phonenumber;
                foundAnnouncement.PostDate = DateTime.Now;
                foundAnnouncement.ExpirationDate = DateTime.Now.AddMonths(1);
                ctx.SaveChanges();

               
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


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

                var data = new { Id = announcement.AnnouncementId, CategoryName = announcement.Category.Name, CategoryId = announcement.Category.CategoryId, ExpirationDate = announcement.ExpirationDate, PostDate = announcement.PostDate, Phonenumber = announcement.Phonenumber, Title = announcement.Title, Closed = announcement.Closed, Description = announcement.Description, Email = announcement.Email };

                return Request.CreateResponse(HttpStatusCode.OK, data);
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
                    ExpirationDate = DateTime.Now.AddMonths(1)
                };

                using (SummerCampDbContext ctx = new SummerCampDbContext())
                {
                    ctx.Announcements.Add(entity);
                    ctx.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, announcement);
                string uri = Url.Link("DefaultApi", new { id = entity.AnnouncementId });
                response.Headers.Location = new Uri(uri);
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

                if (string.Equals(announcement.Email, authInfo.Email, StringComparison.OrdinalIgnoreCase))
                {
                    announcement.Closed = true;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }

        [HttpPost]
        [Route("api/Announcements/ExtendAnnouncement")]
        public HttpResponseMessage ExtendAnnouncement([FromUri] int announcementId, [FromBody] AnnouncementAuthDTO authInfo)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement announcement = ctx.Announcements.FirstOrDefault(a => a.AnnouncementId == announcementId);

                if (string.Equals(announcement.Email, authInfo.Email, StringComparison.OrdinalIgnoreCase))
                {
                    if (announcement.Closed)
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
    }
}
