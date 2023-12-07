using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SHOP_MVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartController(IUserRepository userRepository, IMapper mapper, IProductRepository productRepository, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var products = await _cartRepository.GetCartItemsAsync(email);
            return View(products);
        }
        public async Task<IActionResult> Add(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var products = await _cartRepository.GetCartItemsAsync(email);
            if (products.Any(p => p.Product.Equals(product)))
            {
                await Update(productId, "increase");
            }
            CartItem item = new CartItem()
            {
                Id = default(int),
                Product = product,
                User = await _userRepository.GetUserAsync(email),
                Quantity = 1
            };
            if (!await _cartRepository.AddAsync(item))
            {
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Home", "Index");
            }
            return RedirectToAction("Index","Home");

        }
        [HttpPost]
        public async Task<IActionResult> Update(int productId, string action)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
