using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteBanHangEntities1 objWebsiteBanHangEntities = new WebsiteBanHangEntities1();
        // GET: Category
        public ActionResult Category()
        {
            var lstCategory = objWebsiteBanHangEntities.C2119110263_Category.ToList();
            return View(lstCategory);
        }

        public ActionResult ProductCategory(int Id)
        {
            CategoryModel objCategoryModel = new CategoryModel();
            var lstProduct = objWebsiteBanHangEntities.C2119110263_Product.Where(n => n.CategoryId == Id).ToList();
            var lstCategory = objWebsiteBanHangEntities.C2119110263_Category.ToList();
            var lstBrand = objWebsiteBanHangEntities.C2119110263_Brand.ToList();
            objCategoryModel.Id = Id;
            objCategoryModel.ListProduct = lstProduct;
            objCategoryModel.ListCategory = lstCategory;
            objCategoryModel.ListBrand = lstBrand;
            return View(objCategoryModel);
        }

        public ActionResult ProductCategoryList(int Id)
        {
            //CategoryModel objModel = new CategoryModel();
            //objModel.ListProduct = objWebsiteBanHangEntities.C2119110263_Product.ToList();
            //objModel.ListBrand = objWebsiteBanHangEntities.C2119110263_Brand.ToList();
            var lstProduct = objWebsiteBanHangEntities.C2119110263_Product.Where(n => n.CategoryId == Id).ToList();
            var lstCategory = objWebsiteBanHangEntities.C2119110263_Category.ToList();
            var lstBrand = objWebsiteBanHangEntities.C2119110263_Brand.ToList();
            return View(lstProduct);
        }
    }
}