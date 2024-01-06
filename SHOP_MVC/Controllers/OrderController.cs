using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;
using System.Security.Claims;

namespace SHOP_MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> CreatePartial()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(string address)
        {
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            var userEmailClaim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var user = await _userRepository.GetUserAsync(userEmailClaim.Value);
            var order = new Order()
            {
                Id = 0,
                Status = "На доставке",
                Address = address,
                User = user,
            };
            var cartItems = await _cartRepository.GetCartItemsAsync(userEmailClaim.Value);
            foreach(var cartItem in cartItems)
            {
                OrderProduct orderProductEntity = new OrderProduct()
                {
                    Order = order,
                    Product = cartItem.Product,
                    Quantity = cartItem.Quantity,
                };
                orderProducts.Add(orderProductEntity);
            }
            if (!await _orderRepository.AddOrderAsync(orderProducts, order))
            {
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Index", "Cart");
            }
            TempData["success"] = "Success";
            return RedirectToAction("Index", "Home");
        }

    }
}
