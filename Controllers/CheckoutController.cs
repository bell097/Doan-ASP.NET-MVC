using Doan_ASP.NET_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan_ASP.NET_MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private ShopModelContext db = new ShopModelContext();
        // GET: Checkout
        [Authorize]
        public ActionResult Checkout()
        {
           
            return View();
           
        }
        public ActionResult Order()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return PartialView("Order",giohang);
        } 

        public ActionResult thanhtoan(FormCollection f)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            long tongtien = giohang.Sum(m => m.ThanhTien);
            Bill bill = new Bill();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            bill.user_id = user.Id;
            bill.name = f["firstname"] +" "+ f["lastname"] ;
            bill.phone = f["phone"];
            DateTime dt = DateTime.Now;
            String date1 = dt.ToString("yyyy/MM/dd");
            bill.date = DateTime.ParseExact(date1, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            bill.address = f["address"];
            bill.total = tongtien;
            bill.payment = f["payment"];
            db.Bills.Add(bill);
            db.SaveChanges();
            Session["giohang"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}