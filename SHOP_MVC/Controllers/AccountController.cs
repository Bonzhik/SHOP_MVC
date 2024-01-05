using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models.Dto;
using SHOP_MVC.Models;
using System.Security.Claims;

namespace SHOP_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var userCheck = await _userRepository.GetUserAsync(userDto.Email);
            if (userCheck == null)
            {
                TempData["error"] = "Incorrect email or password";
                return View("Login");
            }
            if (userDto.Password != userCheck.Password)
            {
                TempData["error"] = "Incorrect email or password";
                return View("Login");
            }

            var claim = Authenticate(userCheck);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claim));
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var userCheck = await _userRepository.GetUserAsync(userDto.Email);
            if (userCheck != null)
            {
                TempData["error"] = "User with this email already exists";
                return View("Register");
            }
            var user = _mapper.Map<User>(userDto);
            user.Role = await _roleRepository.GetRoleAsync("User");
            if (!await _userRepository.AddAsync(user))
            {
                TempData["error"] = "Something went wrong while saving";
                return View("Register");
            }
            var claim = Authenticate(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claim));
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Title)
        };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }

}
