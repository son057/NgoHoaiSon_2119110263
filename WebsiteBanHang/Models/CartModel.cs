using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
    public class CartModel
    {
        public C2119110263_Product C2119110263_Product { get; set; }

        public int PriceProduct { get; set; }

        public int Quantity { get; set; }

        public float SumMoney
        {
            get { return PriceProduct * Quantity; }
        }
    }
}