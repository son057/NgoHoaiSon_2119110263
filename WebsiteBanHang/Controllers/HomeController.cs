using Facebook;
using Newtonsoft.Json;
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
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;
using System.Web.Security;

namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();

        public ActionResult Index(string SearchString = "")
        {
            //var lstProduct = new List<C2119110263_Product>();
            //if (page == null) page = 1;

            HomeModel objHomeModel = new HomeModel();
            if (SearchString != "")
            {
                objHomeModel.ListProduct = objwebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Name.Contains(SearchString) || n.NameUnsigned.Contains(SearchString)).ToList();
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

            //lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            //int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            //int pageNumber = (page ?? 1);
            return View(objHomeModel);
        }

        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
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

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            string lastname = me.last_name;
            string picture = me.picture.data.url;
            FormsAuthentication.SetAuthCookie(email, false);
            //if (!string.IsNullOrEmpty(accessToken))
            //{
            //    fb.AccessToken = accessToken;
            //    // Get the user's information, like email, first name, middle name etc
            //    dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            //    string email = me.email;
            //    string userName = me.email;
            //    string firstname = me.first_name;
            //    string middlename = me.middle_name;
            //    string lastname = me.last_name;


            //    objUser.Email = email;
            //    //user.UserName = email;
            //    //user.Status = true;
            //    objUser.FirstName = firstname;
            //    objUser.LastName = lastname;
            //    //user.CreatedDate = DateTime.Now;
            //    objwebsiteBanHangEntities1.C2119110263_Users.Add(objUser);
            //    objwebsiteBanHangEntities1.SaveChanges();
            //    //var data = objwebsiteBanHangEntities1.C2119110263_Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
            //    //if (objUser > 0)
            //    //{
            //    //    var userSession = new UserLogin();
            //    //    userSession.UserName = user.UserName;
            //    //    userSession.UserID = user.ID;
            //    //    Session.Add(CommonConstants.USER_SESSION, userSession);
            //    //}
            //}
            return RedirectToAction("Index","Home");
        }

        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Home/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("son0128525@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "son123456#"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //Verify Email
            //Generate Reset password link
            //Send Email
            string message = "";
            bool status = false;

            using (WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2())
            {
                var account = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.Email == Email).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    objwebsiteBanHangEntities1.Configuration.ValidateOnSaveEnabled = false;
                    objwebsiteBanHangEntities1.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }



        public ActionResult ResetPassword(string Id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(Id))
            {
                return HttpNotFound();
            }

            using (WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2())
            {
                var user = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.ResetPasswordCode == Id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = Id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2())
                {
                    var user = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = GetMD5(model.NewPassword);
                        user.ResetPasswordCode = "";
                        objwebsiteBanHangEntities1.Configuration.ValidateOnSaveEnabled = false;
                        objwebsiteBanHangEntities1.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Contact(C2119110263_Contact cot)
        {
            objwebsiteBanHangEntities1.C2119110263_Contact.Add(cot);
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {

            return View();
        }
    }
}