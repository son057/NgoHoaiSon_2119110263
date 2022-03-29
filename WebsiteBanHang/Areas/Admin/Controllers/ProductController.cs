using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.Common;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();

        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<C2119110263_Product>();
            if (SearchString !=null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();

            //var lstProduct = objwebsiteBanHangEntities1.C2119110263_Product.ToList();
            //var lstProduct = objwebsiteBanHangEntities1.C2119110263_Product.Where(n=>n.Name.Contains(SearchString)).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(C2119110263_Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objWebsiteBanHangEntities1.C2119110263_Product.Add(objProduct);
                    objWebsiteBanHangEntities1.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                    return View();
                }
            }
            return View(objProduct);
            
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(C2119110263_Product objPro)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objWebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(C2119110263_Product objProduct)
        {
            if (objProduct.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                fileName = fileName + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objProduct.CreatedOnUtc = DateTime.Now;
            objWebsiteBanHangEntities1.Entry(objProduct).State = EntityState.Modified;
            objWebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();
            var lstCat = objWebsiteBanHangEntities1.C2119110263_Category.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Brand.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");


            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType lstproductType = new ProductType();
            ProductType objproductType = new ProductType();
            objproductType.Id = 01;
            objproductType.Name = "Giảm giá sốc";
            lstProductType.Add(objproductType);

            objproductType = new ProductType();
            objproductType.Id = 02;
            objproductType.Name = "Đề xuất";
            lstProductType.Add(objproductType);


            DataTable dtProductType = converter.ToDataTable(lstProductType);

            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}