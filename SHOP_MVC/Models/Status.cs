﻿namespace SHOP_MVC.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}