﻿using System.ComponentModel.DataAnnotations;

namespace SHOP_MVC.Models
{
    public class Category
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

