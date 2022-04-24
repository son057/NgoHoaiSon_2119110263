﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebsiteBanHang.Common;
using PagedList;
using PagedList.Mvc;
using WebsiteBanHang.Context;
using ClosedXML.Excel;
using WebsiteBanHang.Library;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();

        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page,string sortOrder)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.PriceDiscountSortParm = sortOrder == "PriceDiscount" ? "PriceDiscount_desc" : "PriceDiscount";
            


            var emp = from e in objWebsiteBanHangEntities1.C2119110263_Product select e;
            

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
                emp = objWebsiteBanHangEntities1.C2119110263_Product.Where(e => e.Name.Contains(SearchString) || e.NameUnsigned.Contains(SearchString));
                emp = emp.OrderByDescending(e => e.Id);
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                emp = objWebsiteBanHangEntities1.C2119110263_Product.Where(e=> e.Deleted == false);

            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //sắp xếp sản phẩm theo Tên, Giá, Giảm Giá

            switch (sortOrder)
            {
                case "name_desc":
                    emp = emp.OrderByDescending(e => e.Name);
                    break;
                case "Price":
                    emp = emp.OrderBy(e => e.Price);
                    break;
                case "Price_desc":
                    emp = emp.OrderByDescending(e => e.Price);
                    break;
                case "PriceDiscount":
                    emp = emp.OrderBy(e => e.PriceDiscount);
                    break;
                case "PriceDiscount_desc":
                    emp = emp.OrderByDescending(e => e.PriceDiscount);
                    break;
                default:
                    emp = emp.OrderByDescending(e => e.Id);
                    break;
            }

            return View(emp.ToPagedList(pageNumber, pageSize));
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
                    //Xử lý thêm Slug
                    objProduct.Slug = XString.Str_Slug(objProduct.Name);
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objProduct.Deleted = false;
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
            //objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objProduct.Deleted = true;
            //objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objWebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(C2119110263_Product objProduct)
        {
            //this.LoadData();
            objProduct.Slug = XString.Str_Slug(objProduct.Name);
            if (objProduct.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                fileName = fileName + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }

            objProduct.UpdatedOnUtc = DateTime.Now;
            objProduct.Deleted = false;
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

        [HttpGet]
        public ActionResult Trash()
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Deleted == true).ToList();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Recover(C2119110263_Product objPro)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            //objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objProduct.Deleted = false;
            //objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objWebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult DeleteTrash(int id)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult DeleteTrash(C2119110263_Product objPro)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            //objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            //objWebsiteBanHangEntities1.C2119110263_Product.Remove(objProduct);
            objWebsiteBanHangEntities1.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult DelTrash(C2119110263_Product objProduct,int?id)
        //{
        //    if(id == null)
        //    {
        //        return RedirectToAction("Index", "Product");
        //    }
        //    objProduct.Deleted = 1;//trạng thái rác = 0
        //    //objProduct.UpdatedOnUtc = Convert.ToInt32(objProduct.UpdatedOnUtc.ToString());
        //    objProduct.CreatedOnUtc = DateTime.Now;
        //    return RedirectToAction("Index", "Product");
        //}

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[15] {
                new DataColumn("Name"),
                new DataColumn("Avatar"),
                new DataColumn("CategoryId"),
                new DataColumn("ShortDes"),
                new DataColumn("FullDes"),
                new DataColumn("Price"),
                new DataColumn("PriceDiscount"),
                new DataColumn("TypeId"),
                new DataColumn("Slug"),
                new DataColumn("BrandId"),
                new DataColumn("Deleted"),
                new DataColumn("ShowOnHomePage"),
                new DataColumn("DisplayOrder"),
                new DataColumn("CreatedOnUtc"),
                new DataColumn("UpdatedOnUtc"),
            });
            var emps = from emp in objWebsiteBanHangEntities1.C2119110263_Product.ToList() select emp;
            foreach (var emp in emps)
            {
                dt.Rows.Add(emp.Name,emp.Avatar,emp.CategoryId,emp.ShortDes,emp.FullDes,emp.Price,emp.PriceDiscount,emp.TypeId,emp.Slug,emp.BrandId,emp.Deleted,emp.ShowOnHomePage,emp.DisplayOrder,emp.CreatedOnUtc,emp.UpdatedOnUtc);
            }
            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                };
            }    
        }
    }
}