﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceLiteEntity.Models;

namespace EcommerceLiteEntity.ViewModels
{
    public class ProductCountModel
    {
        public CategoryViewModel BaseCategory { get; set; }
        public string BaseCategoryName { get; set; }
        public int ProductCount { get; set; }
    }
}
