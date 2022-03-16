﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
    public class CategoryModel
    {
        public List<C2119110263_Product> ListProduct { get; set; }

        public List<C2119110263_Category> ListCategory { get; set; }
        public List<C2119110263_Brand> ListBrand { get; set; }
    }
}