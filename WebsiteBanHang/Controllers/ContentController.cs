using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class ContentController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Content
        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Page()
        {
            var lstPage = objWebsiteBanHangEntities1.C2119110263_Page.ToList();
            return View(lstPage);
        }

        public ActionResult DetailPage(int Id,string tintuc)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var lstPage = objWebsiteBanHangEntities1.C2119110263_Page.Where(n => n.Id == Id).ToList();
            if (lstPage == null)
            {
                return HttpNotFound();
            }
            return View(lstPage);
        }
    }
}