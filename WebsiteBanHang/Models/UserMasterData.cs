using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public partial class UserMasterData
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string LastName{ get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        [Required]
        public Nullable<bool> IsAdmin { get; set; }

        [Required]
        public Nullable<bool> Sex { get; set; }
    }
}