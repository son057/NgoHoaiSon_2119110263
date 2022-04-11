using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class BrandController : Controller
    {
        WebsiteBanHangEntities2 objWebsiteBanHangEntities1 = new WebsiteBanHangEntities2();
        // GET: Brand
        public ActionResult Brand()
        {
            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Brand.ToList();
            return View(lstBrand);
        }

        public ActionResult ProductBrand(int Id,string ThuongHieu)
        {
            //Kiểm tra tham số truyền vào có rỗng hay không
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            //Nếu không thì truy xuất csdl lấy ra sản phẩm tương ứng
            var lstBrand = objWebsiteBanHangEntities1.C2119110263_Product.Where(n => n.BrandId == Id).ToList();

            if (lstBrand == null)
            {
                return HttpNotFound();
            }
            return View(lstBrand);
        }
    }
}