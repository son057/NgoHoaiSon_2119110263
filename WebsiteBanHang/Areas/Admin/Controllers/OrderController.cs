using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var lstOrder = objwebsiteBanHangEntities1.C2119110263_Order.ToList();
            return View(lstOrder);
        }

        public ActionResult Detail(int id)
        {
            var listOrder = objwebsiteBanHangEntities1.C2119110263_OrderDetail.Where(n => n.Id == id).FirstOrDefault();
            return View(listOrder);
        }
    }
}