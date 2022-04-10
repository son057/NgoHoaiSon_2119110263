using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
//using WebsiteBanHang.Context.Models;
using System.Web.Script.Serialization;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {

        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();

        private WebsiteBanHangEntities2 _context;
        public HomeAdminController()
        {
            _context = new WebsiteBanHangEntities2();
        }
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            //if(Session["UserAdmin"].ToString()=="")
            //{
            //    //Chuyển hướng website
            //    Redirect("~/Admin/login");
            //}
            ViewBag.PageView = HttpContext.Application["PageView"].ToString(); // Lấy số lượng người truy cập từ application
            ViewBag.Online = HttpContext.Application["Online"].ToString(); // Lấy số lượng người online từ application
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();// Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang(); // Thống kê đơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien(); // Thống Kê thành viên
            return View();
        }


        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objwebsiteBanHangEntities1.C2119110263_Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.IsAdmin == true).ToList();

                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    //Session["IsAdmin"] = data.FirstOrDefault().IsAdmin;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }




        [HttpGet]
        public JsonResult LoadData(string name, int page, int pageSize = 3)
        {
            IQueryable<C2119110263_Category> model = _context.C2119110263_Category;

            if (!string.IsNullOrEmpty(name))
                model = model.Where(x => x.Name.Contains(name));

            //if (!string.IsNullOrEmpty(status))
            //{
            //    var statusBool = bool.Parse(status);
            //    //model = model.Where(x => x.Status == statusBool);
            //}

            int totalRow = model.Count();

            model = model.OrderByDescending(x => x.CreatedOnUtc)
              .Skip((page - 1) * pageSize)
              .Take(pageSize);


            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
            
        }

        public decimal ThongKeTongDoanhThu()
        {
            
            //Thống kê theo tất cả doanh thu
            decimal TongDoanhThu = decimal.Parse(objwebsiteBanHangEntities1.C2119110263_OrderDetail.Sum(n => n.Price * n.Quantity).ToString());
            return TongDoanhThu;
        }
        public double ThongKeDonHang()
        {
            //Đếm đơn đặt hàng
            double slDDH = objwebsiteBanHangEntities1.C2119110263_Order.Count();
            return slDDH;
        }

        public double ThongKeThanhVien()
        {
            //Đếm đơn đặt hàng
            double slTV = objwebsiteBanHangEntities1.C2119110263_Users.Count();
            return slTV;
        }
    }
}