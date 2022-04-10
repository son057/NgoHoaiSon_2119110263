using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Context
{

    [MetadataType(typeof(UsersMasterData))]

    public partial class C2119110263_Users
    {

    }


    [MetadataType(typeof(ProductMasterData))]

    public partial class C2119110263_Product
    {
        [NotMapped]

        public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }

        //internal static object Include(Func<object, object> p)
        //{
        //    throw new NotImplementedException();
        //}
    }


    [MetadataType(typeof(CategoryMasterData))]

    public partial class C2119110263_Category
    {
        [NotMapped]

        public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
    }


    [MetadataType(typeof(BrandMasterData))]

    public partial class C2119110263_Brand
    {
        [NotMapped]

        public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
    }



    [MetadataType(typeof(PageMasterData))]

    public partial class C2119110263_Page
    {
        [NotMapped]

        public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
    }


    [MetadataType(typeof(OrderMasterData))]

    public partial class C2119110263_Order
    {
       
    }
}