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
        public async Task<IActionResult> CreateOrder(string address, string status)
        {
            var userEmailClaim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var user = await _userRepository.GetUserAsync(userEmailClaim.Value);
            var order = new Order()
            {
                Id = 0,
                Status = status,
                Address = address,
                User = user,
            };
            return View();
        }

    }
}
