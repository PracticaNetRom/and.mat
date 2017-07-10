using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SummerCamp2017.Models;
using Plugin.RestClient;
using System.Net.Http;
using System.Net;
using Microsoft.Ajax.Utilities;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web.Services.Description;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SummerCamp2017.Controllers
{
    public class AnnouncementsController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(AnnouncementPost model)
        //{
        //    SmtpClient client = new SmtpClient("some.server.com");
        //    //If you need to authenticate
        //    //client.Credentials = new NetworkCredential("username", "password");

        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = "andreea.mateiasi@yahoo.com";
        //    mailMessage.To.Add(model.Email);
        //    mailMessage.Subject = "Hello There";
        //    mailMessage.Body = "thank you for adding this announcement: " + model.Title;

        //    client.Send(mailMessage);

        //    return RedirectToAction("List");
        //}
        public ActionResult Index()
        {
            return View();
        }
        // GET: Announcements

        public List<Announcement> GetAnnouncements()
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/";
            var announcementList = rc.GetAsync();
            return announcementList;
        }

        public List<Category> GetCategories()
        {
            RestClient<Category> rc = new RestClient<Category>();
            rc.WebServiceUrl = "http://localhost:10469/api/categories/";
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/categories";
            var categoryList = rc.GetAsync();
            return categoryList;
        }
        public List<Review> GetReviewsForAnnouncement(int id)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:10469/api/reviews/GetByAnnouncementId?announcementId=" + id;
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/reviews/GetByAnnouncementId?announcementId=" + id;
            var reviewList = rc.GetAsync();
            return reviewList;
        }
        // POST: Announcement
        public HttpResponseMessage PostAnnouncement(AnnouncementPost a)
        {
            RestClient<AnnouncementPost> rc = new RestClient<AnnouncementPost>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/NewAnnouncement";
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/newannouncement";
            HttpResponseMessage response = rc.PostAsync(a);
            var id = response.Content.ReadAsStringAsync().Result;
            //string id = Request.Params["id"];
            //string type = Request.QueryString["id"];
            return response;
        }
        public AnnouncementReturn PostAnnouncementt(AnnouncementPost a)
        {
            RestClient<AnnouncementPost> rc = new RestClient<AnnouncementPost>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/NewAnnouncement";
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/newannouncement";
            HttpResponseMessage response = rc.PostAsync(a);
            var id = response.Content.ReadAsStringAsync().Result;
            //var c = JsonConvert.DeserializeObject<List<AnnouncementPost>>(id);
            AnnouncementReturn taskModels = JsonConvert.DeserializeObject<AnnouncementReturn>(id);
            //string c = JsonConvert.DeserializeObject<string>(id);

            return taskModels;
        }
        public AnnouncementDetails GetAnnouncementById(int id)
        {
            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/"+id;

            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/" + id;
            var announcement = rc.GetByIdAsyncc();
            return announcement;
        }
        public AnnouncementPost GetAnnouncementByIdw(int id)
        {
            RestClient<AnnouncementPost> rc = new RestClient<AnnouncementPost>();
            rc.WebServiceUrl = "http://localhost:10469/api/announcements/";
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/announcements/";

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

        public HttpResponseMessage CloseAnnouncement(CloseAnnouncement email)
        {

            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/CloseAnnouncement/" + email.Id;
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/Announcements/CloseAnnouncement/" + email.Id;

            HttpResponseMessage response = rc.Close(email);

            return response;
        }
        public HttpResponseMessage ActivateAnn(CloseAnnouncement email)
        {
            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
             rc.WebServiceUrl = "http://localhost:10469/api/Announcements/ActivateAnnouncement/" + email.Id;
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/Announcements/ActivateAnnouncement/" + email.Id;

            HttpResponseMessage response = rc.Activate(email);

            return response;
        }
        public HttpResponseMessage ExtendAnnouncement(CloseAnnouncement email)
        {

            RestClient<AnnouncementDetails> rc = new RestClient<AnnouncementDetails>();
            rc.WebServiceUrl = "http://localhost:10469/api/Announcements/ExtendAnnouncement/" + email.Id;
            //rc.WebServiceUrl = "http://api.summercamp.stage02.netromsoftware.ro/api/Announcements/ExtendAnnouncement/" + email.Id;

            HttpResponseMessage response = rc.Extend(email);

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
        public async Task<ActionResult> Create(AnnouncementPost model)
        {
            var a = PostAnnouncementt(model);
            try
            {
                string body = "<a href='http://localhost:4116/announcements/ActivateAnnouncementEmail/?id=" + a.AnnouncementId + "&email=" + a.Email + "'> confirm announcement</a>";
                var MailHelper = new MailHelper
                {
                    Sender = "mateiasiandreea19@gmail.com", //email.Sender,
                    Recipient = model.Email,
                    RecipientCC = null,
                    Subject = model.Title + "for sale",
                    Body = body
                };
                MailHelper.Send();

                
            }
            catch (Exception e)
            {
                ModelState.AddModelError("err", e);
                return View();
            }
            return RedirectToAction("List");
        }
        public ActionResult ActivateAnnouncementEmail(int id, string email)
        {
            CloseAnnouncement model = new CloseAnnouncement();
            model.Id = id;
            model.Email = email;
            AnnouncementDetails a = GetAnnouncementById(model.Id);
            HttpResponseMessage response = ActivateAnn(model);
            var s = response.StatusCode.ToString();
            if (s == "OK" && a.Closed == false)
            {
                return RedirectToAction("List");
            }
            else
            {
                if (s == "Forbidden" && a.Closed == false)
                {
                    ModelState.AddModelError("err", "email is not valid");
                    return View();
                }
                if (a.Closed == true)
                {
                    ModelState.AddModelError("err", "the announceent is closed");
                    return View();
                }
                return View();
            }
        
           // return RedirectToAction("List");
        }
        public ActionResult Details(int id)
        {

            List<Review> listReviews = GetReviewsForAnnouncement(id);
            
            listReviews.Reverse();

            int sum = 0, j = 0;
            AnnouncementDetails a = GetAnnouncementById(id);
            a.Reviews = listReviews;
            
            if (listReviews.Count > 0)
            {
                foreach (var item in listReviews)
                {
                    j++;
                    sum += item.Rating;
                }
                float ratingAverage = sum / j;
                int rounded = (int)Math.Round(ratingAverage, 0);
                ViewBag.average = rounded;
            }
            
            return View(a);
        }


        public ActionResult List()
        {
            List<Announcement> announcementList = GetAnnouncements();

            return View(announcementList);

        }
       
        public ActionResult Extend(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Extend(CloseAnnouncement model)
        {

            AnnouncementDetails a = GetAnnouncementById(model.Id);
           
            DateTime date1 = a.ExpirationDate;
            DateTime date2 = DateTime.Now;
            int result = DateTime.Compare(date1, date2);
            TimeSpan difference = date2 - date1;
            if (difference.TotalDays >= -3)
                
            {
                HttpResponseMessage response = ExtendAnnouncement(model);
                var s = response.StatusCode.ToString();
                if (s == "OK" && a.Closed == false)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    if (s == "Forbidden" && a.Closed == false)
                    {
                        ModelState.AddModelError("err", "email is not valid");
                        return View();
                    }
                    if (a.Closed == true)
                    {
                        ModelState.AddModelError("err", "the announceent is closed");
                        return View();
                    }
                    return View();
                }
            }
            else
            {
                if (a.Closed == true)
                {
                    ModelState.AddModelError("err", "the announceent is closed");
                    return View();
                }
                else
                {

                     ModelState.AddModelError("err", "you have more than 3 days till the expiration date");
                     return View();

                }
                return View();
            }





        }
        public ActionResult Close(int id)
        {
            CloseAnnouncement closeAnn = new CloseAnnouncement();
            closeAnn.Id = id;
            closeAnn.Email = "";
            return View(closeAnn);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Close(CloseAnnouncement model)
        {
            //AnnouncementDetails announcement = GetAnnouncementById(email.id);
            //{
            AnnouncementDetails a = GetAnnouncementById(model.Id);
            HttpResponseMessage response = CloseAnnouncement(model);
            var s = response.StatusCode.ToString();
            if (s == "OK" && a.Closed == false)
            {
                return RedirectToAction("List");
            }
            else
            {
                if (a.Closed == true)
                {
                    ModelState.AddModelError("err", "this announcement is already closed");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("err", "email not valid");
                    return View();
                }
                return View();
            }
            //}


        }




    }

}