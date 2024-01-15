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
        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var products = await _cartRepository.GetCartItemsAsync(email);
            if (products.Any(p => p.Product.Equals(product)))
            {
                await Update(productId, "increase");
                TempData["success"] = "Success";
                return Ok();
            }
            else 
            {
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
                    return BadRequest();
                }
                TempData["success"] = "Success";
                return Ok();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(int productId, string action)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var products = await _cartRepository.GetCartItemsAsync(email);
            var rightProduct = products.FirstOrDefault(p => p.Id == productId);
            var rightCartItem = await _cartRepository.GetCartItemAsync(rightProduct.Id);
            if (action == "increase")
            {
                rightCartItem.Quantity += 1;
                await _cartRepository.SaveAsync();
                return Ok(new
                {
                    quantity = rightCartItem.Quantity,
                    totalPrice = rightCartItem.Quantity * rightProduct.Product.Price
                });
            } else
            {
                rightCartItem.Quantity -= 1;
                await _cartRepository.SaveAsync();
                return Ok(new
                {
                    quantity = rightCartItem.Quantity,
                    totalPrice = rightCartItem.Quantity * rightProduct.Product.Price
                });
            }
        
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            await _cartRepository.DeleteAsync(productId);
            return Ok();
        }
        public async Task<bool> CheckEnoughProduct(int productId, int productQuantity)
        {

        }
    }
}
