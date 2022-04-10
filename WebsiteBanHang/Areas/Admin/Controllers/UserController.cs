using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Admin/User
        public ActionResult Index()
        {
            var lstUser = objwebsiteBanHangEntities1.C2119110263_Users.ToList();
            return View(lstUser);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Edit(C2119110263_Users objUser)
        {
            objwebsiteBanHangEntities1.Entry(objUser).State = EntityState.Modified;
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(C2119110263_Users objUse)
        {
            var objUser = objwebsiteBanHangEntities1.C2119110263_Users.Where(n => n.Id == objUse.Id).FirstOrDefault();
            objwebsiteBanHangEntities1.C2119110263_Users.Remove(objUser);
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}