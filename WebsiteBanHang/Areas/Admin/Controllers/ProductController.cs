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
    public class ProductController : Controller
    {
        WebsiteBanHangEntities1 objwebsiteBanHangEntities = new WebsiteBanHangEntities1();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var lstProduct = objwebsiteBanHangEntities.C2119110263_Product.ToList();
            return View(lstProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(C2119110263_Product objProduct)
        {
            try
            {
                if(objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"))+ extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objwebsiteBanHangEntities.C2119110263_Product.Add(objProduct);
                objwebsiteBanHangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objwebsiteBanHangEntities.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objwebsiteBanHangEntities.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(C2119110263_Product objPro)
        {
            var objProduct = objwebsiteBanHangEntities.C2119110263_Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objwebsiteBanHangEntities.C2119110263_Product.Remove(objProduct);
            objwebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objwebsiteBanHangEntities.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(C2119110263_Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objwebsiteBanHangEntities.Entry(objProduct).State = EntityState.Modified;
            objwebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}