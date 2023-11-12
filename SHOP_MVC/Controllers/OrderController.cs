using Microsoft.AspNetCore.Mvc;

namespace SHOP_MVC.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
