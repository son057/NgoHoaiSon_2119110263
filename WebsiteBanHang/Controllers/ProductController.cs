using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using PagedList;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Product
        public ActionResult Detail(int Id,string tensp)
        {
            //Kiểm tra tham số truyền vào có rỗng hay không
            if(Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            //Nếu không thì truy xuất csdl lấy ra sản phẩm tương ứng
            var objProduct = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.Id == Id).FirstOrDefault();
            if(objProduct == null)
            {
                return HttpNotFound();
            }
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


        public JsonResult GetSearchValue(string search)
        {
            WebsiteBanHangEntities2 db = new WebsiteBanHangEntities2();
            List<CountryModel> allsearch = db.C2119110263_Country.Where(x => x.CountryName.Contains(search)).Select(x => new CountryModel
            {
                CountryId = x.CountryId,
                CountryName = x.CountryName
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}