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
    public class CategoryController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var lstCategory = objwebsiteBanHangEntities1.C2119110263_Category.ToList();
            return View(lstCategory);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(C2119110263_Category objCategory)
        {
                try
                {
                    if (objCategory.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objCategory.Avartar = fileName;
                        objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objwebsiteBanHangEntities1.C2119110263_Category.Add(objCategory);
                    objwebsiteBanHangEntities1.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objwebsiteBanHangEntities1.C2119110263_Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objwebsiteBanHangEntities1.C2119110263_Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(C2119110263_Category objCat)
        {
            var objCategory = objwebsiteBanHangEntities1.C2119110263_Category.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objwebsiteBanHangEntities1.C2119110263_Category.Remove(objCategory);
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objwebsiteBanHangEntities1.C2119110263_Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(C2119110263_Category objCategory)
        {
            if (objCategory.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                fileName = fileName + extension;
                objCategory.Avartar = fileName;
                objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category/"), fileName));
            }
            objCategory.CreatedOnUtc = DateTime.Now;
            objwebsiteBanHangEntities1.Entry(objCategory).State = EntityState.Modified;
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}