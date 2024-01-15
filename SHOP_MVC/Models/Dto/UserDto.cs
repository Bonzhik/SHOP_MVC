using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SHOP_MVC.Models.Dto
{
    public class UserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
