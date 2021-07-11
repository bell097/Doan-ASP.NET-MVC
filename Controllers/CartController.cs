using Doan_ASP.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan_ASP.NET_MVC.Controllers
{
    public class CartController : Controller
    {
        private ShopModelContext db = new ShopModelContext();
    
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }

        public ActionResult ThemVaoGio(int SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Product p = db.Product.Find(SanPhamID);  // tim sp theo sanPhamID
                if (p.productsale == 0)
                {
                    CartItem newItem = new CartItem()
                    {
                        SanPhamID = p.product_id,
                        TenSanPham = p.product_name,
                        SoLuong = 1,
                        Hinh = p.product_image,
                        DonGia = (int)p.product_price

                    };  // Tạo ra 1 CartItem mới
                    giohang.Add(newItem);
                }
                else {
                    CartItem newItem = new CartItem()
                    {
                        SanPhamID = p.product_id,
                        TenSanPham = p.product_name,
                        SoLuong = 1,
                        Hinh = p.product_image,
                        DonGia = (int)p.productsale

                    };  // Tạo ra 1 CartItem mới
                    giohang.Add(newItem);
                }
                 // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        public ActionResult XoaKhoiGio(int SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (soluongmoi != 0)
            {
                if (itemSua != null)
                {
                    itemSua.SoLuong = soluongmoi;
                }
                return RedirectToAction("Index");
            }
            else {
                CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                if (itemXoa != null)
                {
                    giohang.Remove(itemXoa);
                }
                return RedirectToAction("Index");
            }
               
        }

        public ActionResult Cartmini()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return PartialView("Cartmini",giohang);
        }

        public ActionResult XoaKhoiGiomini(int SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult ThemVaoGio_Detail(int SanPhamID, int soluong)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Product p = db.Product.Find(SanPhamID);  // tim sp theo sanPhamID
                if (p.productsale == 0)
                {
                    CartItem newItem = new CartItem()
                    {
                        SanPhamID = p.product_id,
                        TenSanPham = p.product_name,
                        SoLuong = soluong,
                        Hinh = p.product_image,
                        DonGia = (int)p.product_price

                    };  // Tạo ra 1 CartItem mới
                    giohang.Add(newItem);
                }
                else
                {
                    CartItem newItem = new CartItem()
                    {
                        SanPhamID = p.product_id,
                        TenSanPham = p.product_name,
                        SoLuong = soluong,
                        Hinh = p.product_image,
                        DonGia = (int)p.productsale

                    };  // Tạo ra 1 CartItem mới
                    giohang.Add(newItem);
                }
                // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong+= soluong;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

    }
}