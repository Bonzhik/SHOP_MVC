﻿using System.ComponentModel.DataAnnotations;

namespace SHOP_MVC.Models.Dto
{
    public class UserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
