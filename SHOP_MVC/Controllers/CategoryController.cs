using Microsoft.AspNetCore.Mvc;

namespace SHOP_MVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
