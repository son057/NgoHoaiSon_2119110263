using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class UserMasterData
    {
        [Required]

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName{ get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Nullable<bool> IsAdmin { get; set; }

        [Required]
        public Nullable<bool> Sex { get; set; }
    }
}