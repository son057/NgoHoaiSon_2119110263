using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.Common;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebsiteBanHangEntities2 objwebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Admin/Order
        public ActionResult Index()
        {
            this.LoadData();
            var lstOrder = objwebsiteBanHangEntities1.C2119110263_Order.ToList();
            return View(lstOrder);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objOrder = objwebsiteBanHangEntities1.C2119110263_OrderDetail.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }

        void LoadData()
        {
            Common objCommon = new Common();
            var lstSta = objwebsiteBanHangEntities1.C2119110263_Order.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtSta= converter.ToDataTable(lstSta);
            ViewBag.ListStatus = objCommon.ToSelectList(dtSta, "Id", "Name");

            //Loại sản phẩm
            List<OrderType> lstOrderType = new List<OrderType>();
            OrderType lstorderType = new OrderType();
            OrderType objoderType = new OrderType();
            objoderType.Status= 1;
            objoderType.Name = "Chờ Lấy Hàng";
            lstOrderType.Add(objoderType);

            objoderType = new OrderType();
            objoderType.Status = 2;
            objoderType.Name = "Đang Vận Chuyển";
            lstOrderType.Add(objoderType);

            objoderType = new OrderType();
            objoderType.Status = 3;
            objoderType.Name = "Giao Hàng Thành Công";
            lstOrderType.Add(objoderType);

            objoderType = new OrderType();
            objoderType.Status = 4;
            objoderType.Name = "Đã Hủy";
            lstOrderType.Add(objoderType);


            DataTable dtOrderType = converter.ToDataTable(lstOrderType);

            ViewBag.OrderType = objCommon.ToSelectList(dtOrderType, "Id", "Name");
        }
    }
}