using Doan_ASP.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan_ASP.NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        private ShopModelContext db = new ShopModelContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        public ActionResult Hotproduct()
        {
            var list = from p in db.Product
                       where p.hot_product == true
                       select p;
            return PartialView("Hotproduct", list);
        }

        public ActionResult Quickview()
        {
           

            return PartialView("Quickview");
        }
       

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}