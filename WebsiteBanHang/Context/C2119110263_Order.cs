//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBanHang.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class C2119110263_Order
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string ShipName { get; set; }
        public string ShipMobile { get; set; }
        public Nullable<bool> IsStatus { get; set; }
    }
}
