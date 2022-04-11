using PagedList;
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

        public ActionResult ProductCategory(string currentFilter, int? page,string SearchString="", int Id = 0)
        {
            CategoryModel objCategoryModel = new CategoryModel();
            List<C2119110263_Product> lstProduct = new List<C2119110263_Product>();
            var lstCategory = objWebsiteBanHangEntities1.C2119110263_Category.ToList();
            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Brand.ToList();
            if (SearchString != "")
            {
                page = 1;
                objCategoryModel.ListProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Name.Contains(SearchString) || n.NameUnsigned.Contains(SearchString)).ToList();
                //objCategoryModel.Id = Id;               
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
                SearchString = currentFilter;
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.CategoryId == Id).ToList();
                //objCategoryModel.Id = Id;
                objCategoryModel.ListProduct = lstProduct;
                objCategoryModel.ListCategory = lstCategory;
                objCategoryModel.ListBrand = lstBrand;
                ViewBag.CurrentFilter = SearchString;
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
                lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            }
            
            
            //objCategoryModel.ListProduct.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(objCategoryModel);
        }

        public ActionResult ProductCategoryList(string currentFilter, int? page, int Id=0)
        {
            //CategoryModel objModel = new CategoryModel();
            //objModel.ListProduct = objWebsiteBanHangEntities.C2119110263_Product.ToList();
            //objModel.ListBrand = objWebsiteBanHangEntities.C2119110263_Brand.ToList();

            List<C2119110263_Product> lstProduct = new List<C2119110263_Product>();
            if (Id == 0)
            {
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.ToList();
            }
            else
            {
                page = 1;
                
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.CategoryId == Id).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            var lstCategory = objWebsiteBanHangEntities1.C2119110263_Category.ToList();
            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Brand.ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}