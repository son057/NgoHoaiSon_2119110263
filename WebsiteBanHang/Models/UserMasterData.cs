using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public partial class UsersMasterData
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [Display(Name = "Tên")]
        public string LastName{ get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Vui lòng nhập email hợp lệ")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

      
        [Display(Name = "Loại Thành Viên")]
        public Nullable<bool> IsAdmin { get; set; }
        public string ResetPasswordCode { get; set; }

        //public string ConfirmPassword { get; set; }

        //[Required]
        //public Nullable<bool> Sex { get; set; }
    }
}