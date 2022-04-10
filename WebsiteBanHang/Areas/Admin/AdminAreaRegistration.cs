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