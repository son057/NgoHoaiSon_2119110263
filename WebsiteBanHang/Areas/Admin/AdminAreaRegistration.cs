using System.Web.Mvc;

namespace WebsiteBanHang.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            

            context.MapRoute(
               "page",
               "dashboard/tin-tuc",
               new { controller = "Page", action = "Index", id = UrlParameter.Optional },
               new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
           );

            context.MapRoute(
               "addpage",
               "dashboard/tin-tuc/them-tin-tuc",
               new { controller = "Page", action = "Create", id = UrlParameter.Optional },
               new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
           );


            context.MapRoute(
                "pageDetails",
                "dashboard/chi-tiet-tin-tuc/{Slug}-{Id}",
                new { controller = "Page", action = "Details", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "pageEdits",
                "dashboard/sua-tin-tuc/{Slug}-{Id}",
                new { controller = "Page", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "pageDeletes",
                "dashboard/xoa-tin-tuc/{Slug}-{Id}",
                new { controller = "Page", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );


            context.MapRoute(
               "danhmuc",
               "dashboard/danh-muc",
               new { controller = "Category", action = "Index", id = UrlParameter.Optional },
               new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
           );

            context.MapRoute(
               "themdanhmuc",
               "dashboard/danh-muc/them-danh-muc",
               new { controller = "Category", action = "Create", id = UrlParameter.Optional },
               new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
           );


            context.MapRoute(
                "categoryDetails",
                "dashboard/chi-tiet/{Slug}-{Id}",
                new { controller = "Category", action = "Details", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "categoryEdits",
                "dashboard/sua-loai-san-pham/{Slug}-{Id}",
                new { controller = "Category", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "categoryDeletes",
                "dashboard/xoa-loai-san-pham/{Slug}-{Id}",
                new { controller = "Category", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "brand",
                "dashboard/thuong-hieu",
                new { controller = "Brand", action = "Index", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "addbrand",
                "dashboard/thuong-hieu/them-thuong-hieu",
                new { controller = "Brand", action = "Create", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "brandDetails",
                "dashboard/chi-tiet-thuong-hieu/{Slug}-{Id}",
                new { controller = "Brand", action = "Details", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "brandEdits",
                "dashboard/sua-thuong-hieu/{Slug}-{Id}",
                new { controller = "Brand", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "brandDeletes",
                "dashboard/xoa-thuong-hieu/{Slug}-{Id}",
                new { controller = "Brand", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "product",
                "dashboard/san-pham",
                new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "productDetails",
                "dashboard/san-pham/chi-tiet/{Slug}-{Id}",
                new { controller = "Product", action = "Details", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "productadd",
                "dashboard/san-pham/them-san-pham",
                new { controller = "Product", action = "Create", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "productEdits",
                "dashboard/san-pham/sua-san-pham/{Slug}-{Id}",
                new { controller = "Product", action = "Edit", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "productDeletes",
                "dashboard/san-pham/xoa-san-pham/{Slug}-{Id}",
                new { controller = "Product", action = "Delete", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "trash",
                "dashboard/thung-rac/{Slug}-{Id}",
                new { controller = "Product", action = "Trash", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "da",
                "dashboard",
                new { controller = "HomeAdmin", action = "Index", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] {"WebSiteBanHang.Areas.Admin.Controllers"}
            );
        }
    }
}