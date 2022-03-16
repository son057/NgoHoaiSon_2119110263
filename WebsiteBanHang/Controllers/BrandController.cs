using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class BrandController : Controller
    {
        WebsiteBanHangEntities1 objWebsiteBanHangEntities = new WebsiteBanHangEntities1();
        // GET: Brand
        public ActionResult Brand()
        {
            var lstBrand = objWebsiteBanHangEntities.C2119110263_Brand.ToList();
            return View(lstBrand);
        }
    }
}