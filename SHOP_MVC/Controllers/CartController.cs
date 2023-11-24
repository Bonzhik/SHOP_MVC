using Microsoft.AspNetCore.Mvc;

namespace SHOP_MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index(int userId)
        {
            return View();
        }
    }
}
