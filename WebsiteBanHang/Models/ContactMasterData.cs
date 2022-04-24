using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class ContactMasterData
    {

        public int Id { get; set; }

        [Display(Name = "Họ Tên")]
        public string Name { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }
        
        public string Email { get; set; }
        [Display(Name = "Nội dung")]
        public string Contact { get; set; }
    }
}