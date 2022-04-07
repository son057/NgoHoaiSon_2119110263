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

        public CartModel() { }
        public CartModel(int productid, string name, string img, decimal price, int qty)
        {
            this.C2119110263_Product.Id = productid;

            this.C2119110263_Product.Name = name;

            this.C2119110263_Product.Avatar = img;

            //    this.C2119110263_Product.Price = float.Parse(price);

            //dynamic tien = 0;
            //tien = quantity * db.product.Price;
            //DBNull.Order.Price = tien;

            this.Quantity = qty;

            this.Amount = Convert.ToDecimal(C2119110263_Product.PriceDiscount * Quantity);
        }

    }
}