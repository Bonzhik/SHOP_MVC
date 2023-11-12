using Microsoft.AspNetCore.Mvc;

namespace SHOP_MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
