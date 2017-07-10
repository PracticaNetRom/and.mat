using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SummerCamp2017.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Post anythig you wolud like to sell and we are sure that you will find a buyer!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us !";
            ViewBag.date = DateTime.Now;

            return View();
        }
    }
}