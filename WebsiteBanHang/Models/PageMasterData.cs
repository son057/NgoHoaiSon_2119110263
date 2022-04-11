using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class PageMasterData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên bài viết")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string ShortDes { get; set; }
        [Display(Name = "Mô tả đầy đủ")]
        public string FullDescription { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Hiện thị trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Lệnh hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }

        public string Slug { get; set; }

    }
}