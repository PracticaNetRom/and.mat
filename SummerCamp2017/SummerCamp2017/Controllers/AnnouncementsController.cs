using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SummerCamp2017.Models;
using Plugin.RestClient;


namespace SummerCamp2017.Controllers
{
    public class AnnouncementsController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: Announcements

        public List<Announcement> GetAnnouncements()
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            var announcementList = rc.GetAsync();
            return announcementList;
        }
        // POST: Announcement
        public bool PostAnnouncement(AnnouncementDetails a)
        {
            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            bool response = rc.PostAsync(a);
            return response;
        }
        public AnnouncementDetails GetAnnouncementById(int id)
        {
            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            var announcement = rc.GetByIdAsync(id);
            return announcement;
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {

            AnnouncementDetails a = GetAnnouncementById(id);

            return View(a);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnnouncementDetails a)
        {

            PostAnnouncement(a);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Create(AnnouncementDetails a)
        {

            PostAnnouncement(a);
            return RedirectToAction("List");
        }
        public ActionResult Details(int id)
        {
            AnnouncementDetails a = GetAnnouncementById(id);

            return View(a);
        }


        public ActionResult List()
        {

                List<Announcement> apartmentList = GetAnnouncements();

                return View(apartmentList);
            
            // return View(apartmentList);
        }




    }
}
