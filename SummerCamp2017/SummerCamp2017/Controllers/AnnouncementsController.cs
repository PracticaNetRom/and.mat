using System;
using System.Collections.Generic;
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

        public List<Category> GetCategories()
        {
            RestClient<Category> rc = new RestClient<Category>();
            rc.WebServiceUrl = "http://localhost:10469/api/categories/";
            var categoryList = rc.GetAsync();
            return categoryList;
        }

        // POST: Announcement
        public bool PostAnnouncement(AnnouncementPost a)
        {
            RestClient<AnnouncementPost> rc = new RestClient<AnnouncementPost>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/NewAnnouncement";
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
        public AnnouncementPost GetAnnouncementByIdw(int id)
        {
            RestClient<AnnouncementPost> rc = new RestClient<AnnouncementPost>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            var announcement = rc.GetByIdAsync(id);
            return announcement;
        }
        //public bool PutAnnouncement(int id, AnnouncementPost a)
        //{
        //    RestClient<AnnouncementPost> rc = new RestClient<AnnouncementPost>();
        //    rc.WebServiceUrl = "http://localhost:10469/api/announcements/PutAnnouncements";
        //    bool response = rc.PutAsync(id, a);
        //    return response;
        //}

        public bool CloseAnnouncement(int id, AnnouncementDetails ann)
        {

            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/CloseAnnouncement/" + id;
            bool response = rc.Close(ann);

            return response;
        }
        public bool ExtendAnnouncement(int id, AnnouncementDetails ann)
        {

            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/ExtendAnnouncement/" + id;
            bool response = rc.Extend(ann);

            return response;
        }
        public ActionResult Create()
        {
            List<Category> categoryList = GetCategories();
            ViewBag.data = categoryList;
            return View();
        }

        


       
        public ActionResult Edit(int id)
        {

            AnnouncementPost a = GetAnnouncementByIdw(id);

            return View(a);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnnouncementPost a)
        {

            //PutAnnouncement(a.Id ,a);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Create(AnnouncementPost a)
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

                List<Announcement> announcementList = GetAnnouncements();

                return View(announcementList);

        }
        //public ActionResult DropDownCategories()
        //{

            
        //    return View(ViewBag);

        //}
        public ActionResult Extend(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Extend(int id, string email)
        {
            AnnouncementDetails announcement = GetAnnouncementById(id);
            if (announcement.Email == email)
            {
                bool response = ExtendAnnouncement(id, announcement);
                if (response)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult Close(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Close(int id, string email)
        {
            AnnouncementDetails announcement = GetAnnouncementById(id);
            if (announcement.Email == email)
            {
                bool response = CloseAnnouncement(id, announcement);
                if (response)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            else
            {
                return HttpNotFound();
            }

        }


    }
}
