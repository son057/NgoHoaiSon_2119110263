using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class OrderMasterData
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Trạng Thái")]
        public Nullable<int> Status { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Mã thành viên")]
        public Nullable<int> UserId { get; set; }
        
        public string Email { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }
        [Display(Name = "Mã khách hàng")]
        public Nullable<int> CustomerID { get; set; }
        [Display(Name = "Tên Khách Hàng")]
        public string ShipName { get; set; }
        [Display(Name = "Số điện thoại")]
        public string ShipMobile { get; set; }
        
        public Nullable<bool> IsStatus { get; set; }
    }
}