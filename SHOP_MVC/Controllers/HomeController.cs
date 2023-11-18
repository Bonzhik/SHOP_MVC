using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;
using SHOP_MVC.Models.Dto;
using SHOP_MVC.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace SHOP_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public HomeController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var homeVM = new HomePageView()
            {
                Categories = _mapper.Map<ICollection<CategoryDto>>(await _categoryRepository.GetCategoriesAsync()),
                Products = _mapper.Map<ICollection<ProductDto>>(await _productRepository.GetProductsAsync())
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}