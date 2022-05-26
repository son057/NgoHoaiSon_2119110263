using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getChartData()
        {
            List<C2119110263_Employees> list = new List<C2119110263_Employees>();
            using (WebsiteBanHangEntities2 dc = new WebsiteBanHangEntities2())
            {
                list = dc.C2119110263_Employees.ToList();
            }
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }    
    }
}