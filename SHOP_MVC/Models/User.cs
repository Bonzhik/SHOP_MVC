﻿using Microsoft.AspNetCore.Identity;

namespace SHOP_MVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
