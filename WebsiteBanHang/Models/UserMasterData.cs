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
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Loại Thành Viên")]
        public Nullable<bool> IsAdmin { get; set; }

        //[Required]
        //public Nullable<bool> Sex { get; set; }
    }
}