using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;
using WebsiteBanHang.common;
using MailHelper = WebsiteBanHang.common.MailHelper;

namespace WebsiteBanHang.Controllers
{
    public class CartController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        //private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            //var lstProduct = new List<CartModel>Session["cart"]();
            return View((List<CartModel>)Session["cart"]);
        }

        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { C2119110263_Product = objWebsiteBanHangEntities1.C2119110263_Product.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { C2119110263_Product = objWebsiteBanHangEntities1.C2119110263_Product.Find(id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }


        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].C2119110263_Product.Id.Equals(id))
                    return i;
            return -1;
        }

        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.C2119110263_Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }





        [HttpGet]
        public ActionResult Payment()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var cart = Session["cart"];
            var list = new List<CartModel>();
            if (cart != null)
            {
                list = (List<CartModel>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            
                var order = new C2119110263_Order();
                var cart = (List<CartModel>)Session["cart"];
                foreach (var item in cart)
                {
                    order.Name = shipName;
                    order.UserId = 3;
                    order.Price = item.C2119110263_Product.Price;
                    order.Status = 1;
                    order.CreatedOnUtc = DateTime.Now;
                    order.Address = address;
                    order.ShipMobile = mobile;
                    order.ShipName = shipName;
                    order.Email = email;
                }

                try
                {

                    //Thêm Order               
                    objWebsiteBanHangEntities1.C2119110263_Order.Add(order);
                    objWebsiteBanHangEntities1.SaveChanges();
                    var id = order.Id;

                    //var cart = (List<CartModel>)Session["cart"];

                    decimal total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new C2119110263_OrderDetail();
                        orderDetail.ProductId = item.C2119110263_Product.Id;
                        orderDetail.OrderId = id;
                        orderDetail.Price = item.C2119110263_Product.Price;
                        orderDetail.Quantity = item.Quantity;
                        objWebsiteBanHangEntities1.C2119110263_OrderDetail.Add(orderDetail);
                        objWebsiteBanHangEntities1.SaveChanges();
                        total += Convert.ToDecimal(item.C2119110263_Product.PriceDiscount.GetValueOrDefault(0) * item.Quantity);
                    }
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

                    content = content.Replace("{{CustomerName}}", shipName);
                    content = content.Replace("{{Phone}}", mobile);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{Address}}", address);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
                    //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
                    //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
                    //của ứng dụng kém an toàn phải ở chế độ bật
                    //  Đồng thời tài khoản Gmail cũng cần bật IMAP
                    //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

                    new MailHelper().SendMail(email, "Đơn hàng mới", content);
                    new MailHelper().SendMail(toEmail, "Đơn hàng mới", content);
                    //Xóa hết giỏ hàng
                    Session["cart"] = null;
                }
                catch (Exception ex)
                {
                    //ghi log
                    return Redirect("/Cart/UnSuccess");
                }
            return Redirect("/Cart/Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult UnSuccess()
        {
            return View();
        }

    }
}