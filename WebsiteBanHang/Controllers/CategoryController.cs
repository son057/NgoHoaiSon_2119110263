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
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Category
        public ActionResult Category()
        {
            var lstCategory = objWebsiteBanHangEntities1.C2119110263_Category.ToList();
            return View(lstCategory);
        }

        public ActionResult ProductCategory(string SearchString="", int Id = 0)
        {
            CategoryModel objCategoryModel = new CategoryModel();
            List<C2119110263_Product> lstProduct = new List<C2119110263_Product>();
            var lstCategory = objWebsiteBanHangEntities1.C2119110263_Category.ToList();
            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Brand.ToList();
            if (SearchString != "")
            {
                objCategoryModel.ListProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Name.Contains(SearchString)).ToList();
                objCategoryModel.Id = Id;               
                objCategoryModel.ListCategory = lstCategory;
                objCategoryModel.ListBrand = lstBrand;
                return View(objCategoryModel);
            }    
            if (Id == 0)
            {
                lstProduct =  objWebsiteBanHangEntities1.C2119110263_Product.ToList();
            }
            else
            {
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.CategoryId == Id).ToList();
            }
            
            objCategoryModel.Id = Id;
            objCategoryModel.ListProduct = lstProduct;
            objCategoryModel.ListCategory = lstCategory;
            objCategoryModel.ListBrand = lstBrand;
            //objCategoryModel.ListProduct.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(objCategoryModel);
        }

        public ActionResult ProductCategoryList(int Id)
        {
            //CategoryModel objModel = new CategoryModel();
            //objModel.ListProduct = objWebsiteBanHangEntities.C2119110263_Product.ToList();
            //objModel.ListBrand = objWebsiteBanHangEntities.C2119110263_Brand.ToList();
            var lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.CategoryId == Id).ToList();
            var lstCategory = objWebsiteBanHangEntities1.C2119110263_Category.ToList();
            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Brand.ToList();
            return View(lstProduct);
        }
    }
}