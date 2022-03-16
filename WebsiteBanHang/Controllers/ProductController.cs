using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities1 objWebsiteBanHangEntities = new WebsiteBanHangEntities1();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.C2119110263_Product.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}