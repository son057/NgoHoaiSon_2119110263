using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
    [Serializable]
    public class CartModel
    {
        public C2119110263_Product C2119110263_Product { get; set; }

        //public int PriceProduct { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        
        public decimal Amount { get; set; }

        public C2119110263_Users C2119110263_Users { get; set; }
    }
}