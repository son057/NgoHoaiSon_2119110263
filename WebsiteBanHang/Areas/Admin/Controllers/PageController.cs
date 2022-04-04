using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class PageController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Admin/Page
        public ActionResult Index()
        {
            var lstPage = objwebsiteBanHangEntities1.C2119110263_Page.ToList();
            return View(lstPage);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(C2119110263_Page objPage)
        {
            try
            {
                if (objPage.ImageUpLoad != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objPage.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objPage.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objPage.Avatar = fileName;
                    objPage.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/avatars/"), fileName));
                }
                objPage.CreatedOnUtc = DateTime.Now;
                objwebsiteBanHangEntities1.C2119110263_Page.Add(objPage);
                objwebsiteBanHangEntities1.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            return View(objPage);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objPage = objwebsiteBanHangEntities1.C2119110263_Page.Where(n => n.Id == id).FirstOrDefault();
            return View(objPage);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objPage = objwebsiteBanHangEntities1.C2119110263_Page.Where(n => n.Id == id).FirstOrDefault();
            return View(objPage);
        }


        [HttpPost]
        public ActionResult Delete(C2119110263_Page objPag)
        {
            var objPage = objwebsiteBanHangEntities1.C2119110263_Page.Where(n => n.Id == objPag.Id).FirstOrDefault();
            objwebsiteBanHangEntities1.C2119110263_Page.Remove(objPage);
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objPage = objwebsiteBanHangEntities1.C2119110263_Page.Where(n => n.Id == id).FirstOrDefault();
            return View(objPage);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(C2119110263_Page objPage)
        {
            if (objPage.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objPage.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objPage.ImageUpLoad.FileName);
                fileName = fileName + extension;
                objPage.Avatar = fileName;
                objPage.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/avatars/"), fileName));
            }
            objPage.CreatedOnUtc = DateTime.Now;
            objwebsiteBanHangEntities1.Entry(objPage).State = EntityState.Modified;
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}