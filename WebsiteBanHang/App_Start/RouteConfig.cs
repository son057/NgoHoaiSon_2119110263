using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "tintuc",
               url: "tin-tuc",
               defaults: new { controller = "Content", action = "Page", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanHang.Controllers" }
           );


            routes.MapRoute(
               name: "vechungtoi",
               url: "ve-chung-toi",
               defaults: new { controller = "Starter", action = "Starter", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "dangky",
               url: "dang-ky",
               defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "dangnhap",
               url: "dang-nhap",
               defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "chinhsachbanhang",
               url: "chinh-sach-ban-hang",
               defaults: new { controller = "Content", action = "Content", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanHang.Controllers" }
           );
          


            routes.MapRoute(
              name: "lienhe",
              url: "lien-he",
              defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteBanHang.Controllers" }
          );


            routes.MapRoute(
               name: "productall",
               url: "san-pham",
               defaults: new { controller = "Product", action = "AllProduct", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanHang.Controllers" }
           );

            //Cấu hình đường dẫn trang xem thương hiệu
            routes.MapRoute(
              name: "thuonghieu",
              url: "thuong-hieu",
              defaults: new { controller = "Brand", action = "Brand", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteBanHang.Controllers" }
          );


            routes.MapRoute(
              name: "productbrand",
              url: "thuonghieu/{slg}-{id}",
              defaults: new { controller = "Brand", action = "ProductBrand", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteBanHang.Controllers" }
          );


            routes.MapRoute(
             name: "content",
             url: "tin-tuc/{slg}-{id}",
             defaults: new { controller = "Content", action = "DetailPage", id = UrlParameter.Optional },
             namespaces: new[] { "WebsiteBanHang.Controllers" }
         );


            routes.MapRoute(
              name: "XemChiTiet",
              url: "{tensp}-{id}",
              defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteBanHang.Controllers" }
          );




            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebsiteBanHang.Controllers" }
            );

        }
    }
}
