using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;
using PagedList;

namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        
        public ActionResult Index(int? page,string SearchString="")
        {
            var lstProduct = new List<C2119110263_Product>();
            if (page == null) page = 1;
            
            HomeModel objHomeModel = new HomeModel();
            if (SearchString !="")
            {
                objHomeModel.ListProduct = objwebsiteBanHangEntities1.C2119110263_Product.Where(n => n.NameUnsigned.Contains(SearchString)).ToList();
                objHomeModel.ListCategory = objwebsiteBanHangEntities1.C2119110263_Category.ToList();
                objHomeModel.ListBrand = objwebsiteBanHangEntities1.C2119110263_Brand.ToList();
                return View(objHomeModel);
            }
            else
            {
               
                objHomeModel.ListCategory = objwebsiteBanHangEntities1.C2119110263_Category.ToList();
                objHomeModel.ListBrand = objwebsiteBanHangEntities1.C2119110263_Brand.ToList();
                objHomeModel.ListProduct = objwebsiteBanHangEntities1.C2119110263_Product.ToList();
            }

            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(C2119110263_Users _user)
        {
            //kiểm tra và lưu về database
            if (ModelState.IsValid)
            {
                var check = objwebsiteBanHangEntities1.C2119110263_Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objwebsiteBanHangEntities1.Configuration.ValidateOnSaveEnabled = false;
                    objwebsiteBanHangEntities1.C2119110263_Users.Add(_user);
                    objwebsiteBanHangEntities1.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objwebsiteBanHangEntities1.C2119110263_Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
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

        //Logout
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
    }
}