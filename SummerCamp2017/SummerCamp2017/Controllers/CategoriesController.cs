using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerCamp2017.Models;
using Plugin.RestClient;

namespace SummerCamp2017.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public List<Category> GetAnnouncements()
        {
            RestClient<Category> rc = new RestClient<Category>();
            rc.WebServiceUrl = "http://localhost:10469/api/categories/";
            var categoryList = rc.GetAsync();
            return categoryList;
        }
    }
}