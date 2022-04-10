using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class CategoryMasterData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên loại phẩm")]
        [Display(Name = "Tên loại sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Avartar { get; set; }
        
        public string Slug { get; set; }
        [Display(Name = "Hiện thị trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Lệnh hiện thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Trạng Thái")]
        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
    }
}