using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Payment

        public ActionResult Index(string name, string address, string email)
        {
            var order = new C2119110263_Order();
            order.Name = name;
            order.Address = address;
            order.Email = email;
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lấy thông tin từ giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["cart"];
                //gán dữ liệu cho bảng Order
                C2119110263_Order objOrder = new C2119110263_Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;

                objWebsiteBanHangEntities1.C2119110263_Order.Add(objOrder);
                //lưu thông tin dữ liệu vào bảng order
                objWebsiteBanHangEntities1.SaveChanges();

                //Lấy OrderId vừa mới tạo để lưu vào bảng OrderDetail
                int intOrderId = objOrder.Id;

                List<C2119110263_OrderDetail> lstOrderDetail = new List<C2119110263_OrderDetail>();

                foreach (var item in lstCart)
                {
                    C2119110263_OrderDetail obj = new C2119110263_OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.C2119110263_Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWebsiteBanHangEntities1.C2119110263_OrderDetail.AddRange(lstOrderDetail);
                objWebsiteBanHangEntities1.SaveChanges();
            }
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}