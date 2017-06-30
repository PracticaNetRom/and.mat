using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.DataAccess;
using API.Models;
using System.Collections.Generic;

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
        [Route("api/Announcements/CloseAnnouncement")]
        public HttpResponseMessage CloseAnnouncement([FromUri] int announcementId, [FromBody] AnnouncementAuthDTO authInfo)
        {
            using (SummerCampDbContext ctx = new SummerCampDbContext())
            {
                Announcement announcement = ctx.Announcements.FirstOrDefault(a => a.AnnouncementId == announcementId);

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
