using Doan_ASP.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan_ASP.NET_MVC.Controllers
{
    public class ProductController : Controller
    {
        private ShopModelContext db = new ShopModelContext();
        // GET: Product
        public ActionResult Product_Index(int? id, int? brandid, int? originid, int? saleid)
        {

            if (id == 0 && brandid == 0 && originid==0 && saleid ==0)
            {
                var categories = from c in db.Categories
                                 where c.category_id == 1
                                 select c;
                return View(categories);
            }
            else if (id != 0 && brandid == 0 && originid == 0 && saleid== 0 )
            {
                var categories = from c in db.Categories
                                 where c.category_id == id
                                 select c;
                return View(categories);
            }
            else if (id != null && brandid != null)
            {
                var categories = from c in db.Categories
                                 where c.category_id == id
                                 select c;
                foreach (Category c in categories)
                {
                    c.brandid = (int)brandid;
                }
                return View(categories);
            }
            else if (id != null && originid != null)
            {
                var categories = from c in db.Categories
                                 where c.category_id == id
                                 select c;
                foreach (Category c in categories)
                {
                    c.originid = (int)originid;
                }
                return View(categories);
            }
            else if (id != 0 && saleid != 0 && originid == null && brandid == null)
            {
                var categories = from c in db.Categories
                                 where c.category_id == id
                                 select c;
                foreach (Category c in categories)
                {
                    c.saleid = (int)saleid;
                    c.brandid = 0;
                    c.originid = 0;
                }
                return View(categories);
            }

            return View();

        }


        public ActionResult Getbrand_product(int id)
        {
            var list = (from b in db.Brands
                        join p in db.Products on b.brand_id equals p.brand_id
                        where p.category_id == id
                        select b).Distinct();

            foreach (Brand b in list)
            {
                b.category1 = id;
            }

            return PartialView("Getbrand_product", list);
        }
        public ActionResult Getorigin_product(int id)
        {
            var list = (from o in db.Origins
                        join p in db.Products on o.origin_id equals p.origin_id
                        where p.category_id == id
                        select o).Distinct();
            foreach(Origin o in list)
            {
                o.category1 = id;
            }

            return PartialView("Getorigin_product", list);
        }
        public ActionResult Getsale_product(int id)
        {
            var list = (from s in db.sales
                        join p in db.Products on s.sale_id equals p.sale_id
                        where p.category_id == id
                        orderby s.sale_name
                        select s).Distinct();
            foreach (sale s in list)
            {
                s.category1 = id;
            }

            return PartialView("Getsale_product", list);
        }


        public ActionResult Getproduct(int? id, int? brandid, int? originid, int? saleid)
        {
            if (id == 0 && brandid == 0 && originid == 0 && saleid == 0)
            {
                IQueryable<Product> list = from p in db.Products
                                           orderby p.product_id
                                           select p;
              
                return PartialView("Getproduct", list);

            }
            else if (id != 0 && saleid != 0)
            {
                IQueryable<Product> list = from p in db.Products
                                           where p.category_id == id && p.sale_id == saleid
                                           select p;
               
                return PartialView("Getproduct", list);
            }
            else if (id != 0 && brandid == 0 && originid == 0 && saleid == 0)
            {
                IQueryable<Product> list = from p in db.Products
                                           where p.category_id == id
                                           orderby p.product_id
                                           select p;
             
                return PartialView("Getproduct", list);
            }
            else if (id != 0 && brandid != 0 && originid == null   )
            {
                IQueryable<Product> list = from p in db.Products
                                           where p.category_id == id && p.brand_id == brandid
                                           select p;
               
                return PartialView("Getproduct", list);
            }else if(id != 0 && originid != 0 && brandid == 0) 
            { 
                IQueryable<Product> list = from p in db.Products
                                           where p.category_id == id && p.origin_id == originid
                                           select p;
               
                return PartialView("Getproduct", list);
            }
      

            return null;
          
        }

        public ActionResult Product_Detail(int id)
        {
            var product = from p in db.Products
                          where p.product_id == id
                          select p;
           
            return View(product);
        }

        public ActionResult Image_Detail(int id)
        {
            var list = from p in db.Products
                       join i in db.Image_detail on p.product_id equals i.product_id
                       where p.product_id == id
                       select i;
            return PartialView("Image_Detail",list);
        }

        public ActionResult Getproduct_same(int id)
        {
            var list = (from p in db.Products
                       where p.category_id == id
                       orderby p.product_id
                       select p).Take(6);
          
        
            return PartialView("Getproduct_same", list);
        }

        public ActionResult GetproductByname( int option ,String tukhoa)
        {
            if (option == 0) {
                var list = (from p in db.Products
                            where p.product_name.StartsWith(tukhoa) || p.Category.category_name.StartsWith(tukhoa)
                            select p).ToList();
                if (list.Count == 0)
                    return RedirectToAction("Error");
                else
                {
                    return PartialView("GetproductByname", list);
                }
            }
            else if (option == 1)
            {
                var list = (from p in db.Products
                            where (p.product_name.StartsWith(tukhoa) || p.Category.category_name.StartsWith(tukhoa)) && p.category_id == option
                            select p).ToList();
                if (list.Count == 0)
                    return RedirectToAction("Error");
               else
                    {
                    return PartialView("GetproductByname", list);
                }
            }
            else if (option == 2)
            {
                var list = (from p in db.Products
                            where (p.product_name.StartsWith(tukhoa) || p.Category.category_name.StartsWith(tukhoa)) && p.category_id == option
                            select p).ToList();
                if (list.Count == 0)
                    return RedirectToAction("Error");
                else
                {
                    return PartialView("GetproductByname", list);
                }
            }
            else if (option == 3)
            {
                var list = (from p in db.Products
                            where (p.product_name.StartsWith(tukhoa) || p.Category.category_name.StartsWith(tukhoa)) && p.category_id == option
                            select p).ToList();
                if (list.Count == 0)
                    return RedirectToAction("Error");
                else
                {
                    return PartialView("GetproductByname", list);
                }
            }
            else if (option == 4)
            {
                var list = (from p in db.Products
                            where (p.product_name.StartsWith(tukhoa) || p.Category.category_name.StartsWith(tukhoa)) && p.category_id == option
                            select p).ToList();
                if (list.Count == 0)
                    return RedirectToAction("Error");
                else
                {
                    return PartialView("GetproductByname", list);
                }
            }
            else if (option == 5)
            {
                var list = (from p in db.Products
                            where (p.product_name.StartsWith(tukhoa)||p.Category.category_name.StartsWith(tukhoa)) && p.category_id == option
                            select p).ToList();
                if (list.Count == 0)
                    return RedirectToAction("Error");
                else
                {
                    return PartialView("GetproductByname", list);
                }
            }


            return RedirectToAction("Error");
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}