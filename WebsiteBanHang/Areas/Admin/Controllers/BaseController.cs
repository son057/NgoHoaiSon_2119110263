using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public  BaseController()
        {
            if (System.Web.HttpContext.Current.Session["idUser"].Equals("")) //kiem tra dang nhap neu = null chuyen huong web
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/login");
            }
        }
    }
}