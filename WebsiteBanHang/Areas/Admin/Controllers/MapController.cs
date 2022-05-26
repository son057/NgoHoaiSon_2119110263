using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class MapController : Controller
    {
        // GET: Admin/Map

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllLocation()
        {
            using (WebsiteBanHangEntities2 dc = new WebsiteBanHangEntities2())
            {
                var v = dc.C2119110263_Locations.OrderBy(a => a.Title).ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        public JsonResult GetMarkerInfo(int LocationID)
        {
            using (WebsiteBanHangEntities2 dc = new WebsiteBanHangEntities2())
            {
                C2119110263_Locations l = null;
                l = dc.C2119110263_Locations.Where(a => a.LocationID.Equals(LocationID)).FirstOrDefault();
                return new JsonResult { Data = l, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}