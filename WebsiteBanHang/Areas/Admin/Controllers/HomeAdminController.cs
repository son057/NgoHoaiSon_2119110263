﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            //if(Session["UserAdmin"].ToString()=="")
            //{
            //    //Chuyển hướng website
            //    Redirect("~/Admin/login");
            //}    
            return View();
        }
    }
}