using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using PagedList;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }

        public ActionResult AllProduct(string currentFilter,int? page, string SearchString = "", int Id = 0)
        {
            var lstProduct = new List<C2119110263_Product>();

            if (SearchString != "")
            {

                page = 1;
                var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == Id).FirstOrDefault();
                lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Name.Contains(SearchString)).ToList();
                return View(lstProduct);
            }
            SearchString = currentFilter;


            ViewBag.CurrentFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            lstProduct = objWebsiteBanHangEntities1.C2119110263_Product.ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}