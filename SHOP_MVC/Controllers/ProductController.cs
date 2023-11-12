using Microsoft.AspNetCore.Mvc;

namespace SHOP_MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
