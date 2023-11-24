using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;
using SHOP_MVC.Models.Dto;
using SHOP_MVC.Models.ViewModels;
using System.Diagnostics;

namespace SHOP_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = _mapper.Map<ICollection<ProductDto>>(await _productRepository.GetProductsAsync());
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> CreatePartial()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var allCategories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            });
            var viewModel = new CEProductViewModel
            {
                AllCategories = allCategories
            };
            return PartialView(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePartial(CEProductViewModel viewModel)
        {
            var product = _mapper.Map<Product>(viewModel.ProductDto);
            List<Category> categories = new List<Category>();
            foreach (var category in viewModel.SelectedCategories)
            {
                categories.Add(await _categoryRepository.GetCategoryAsync(category));
            }
            product.Categories = categories;
            if (await _productRepository.IsExistsAsync(product))
            {
                TempData["error"] = "This item already exists";
                return RedirectToAction("Index");
            }
            if (!await _productRepository.AddAsync(product))
            {
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Success";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditPartial(int id)
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var allCategories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            });
            var viewModel = new CEProductViewModel
            {
                ProductDto = _mapper.Map<ProductDto>(await _productRepository.GetProductAsync(id)),
                AllCategories = allCategories
            };
            return PartialView(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditPartial(CEProductViewModel viewModel)
        {
            var product = await _productRepository.GetProductAsync(viewModel.ProductDto.Id);
            product.Categories.Clear();
            List<Category> categories = new List<Category>();
            foreach (var category in viewModel.SelectedCategories)
            {
                categories.Add(await _categoryRepository.GetCategoryAsync(category));
            }
            product.Categories = categories;
            product.Title = viewModel.ProductDto.Title;
            product.Description = viewModel.ProductDto.Description;
            product.Image = viewModel.ProductDto.Image;
            product.Quantity = viewModel.ProductDto.Quantity;
            product.Price = viewModel.ProductDto.Price;
            if (await _productRepository.IsExistsAsync(product))
            {
                TempData["error"] = "This item already exists";
                return RedirectToAction("Index");
            }
            if (!await _productRepository.UpdateAsync(product))
            {
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Success";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DeletePartial(int id)
        {
            var product = _mapper.Map<ProductDto>(await _productRepository.GetProductAsync(id));
            if (product == null)
            {
                return NotFound();
            }
            return PartialView(product);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePartial(ProductDto productDto)
        {

            if (!await _productRepository.DeleteAsync(productDto.Id))
            {
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Success";
            return RedirectToAction("Index");
        }
    }
}
