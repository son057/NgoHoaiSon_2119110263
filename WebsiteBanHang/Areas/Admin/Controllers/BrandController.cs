using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Library;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var lstBrand = objwebsiteBanHangEntities1.C2119110263_Brand.ToList();
            return View(lstBrand);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(C2119110263_Brand objBrand)
        {
            try
            {
                objBrand.Slug = XString.Str_Slug(objBrand.Name);
                if (objBrand.ImageUpLoad != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand/"), fileName));
                }
                objBrand.CreatedOnUtc = DateTime.Now;
                objwebsiteBanHangEntities1.C2119110263_Brand.Add(objBrand);
                objwebsiteBanHangEntities1.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = objwebsiteBanHangEntities1.C2119110263_Brand.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objwebsiteBanHangEntities1.C2119110263_Brand.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }


        [HttpPost]
        public ActionResult Delete(C2119110263_Brand objBra)
        {
            var objBrand = objwebsiteBanHangEntities1.C2119110263_Brand.Where(n => n.Id == objBra.Id).FirstOrDefault();
            objwebsiteBanHangEntities1.C2119110263_Brand.Remove(objBrand);
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objwebsiteBanHangEntities1.C2119110263_Brand.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Edit(C2119110263_Brand objBrand)
        {
            if (objBrand.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                fileName = fileName + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand/"), fileName));
            }
            objBrand.CreatedOnUtc = DateTime.Now;
            objwebsiteBanHangEntities1.Entry(objBrand).State = EntityState.Modified;
            objwebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}