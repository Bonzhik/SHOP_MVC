using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;
using SHOP_MVC.Models.Dto;

namespace SHOP_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = _mapper.Map<ICollection<CategoryDto>>(await _categoryRepository.GetCategoriesAsync());
            return View(categories);
        }
        [HttpGet]
        public IActionResult CreatePartial()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartial(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            if (await _categoryRepository.IsExistsAsync(category))
            {
                return RedirectToAction("Index");
            }
            if (!await _categoryRepository.AddAsync(category))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditPartial(int id) {
            var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryAsync(id));
            if (category == null)
            {
               return NotFound();
            }
            return PartialView(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditPartial(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            if (await _categoryRepository.IsExistsAsync(category))
            {
                return RedirectToAction("Index");
            }
            if (!await _categoryRepository.UpdateAsync(category))
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DeletePartial(int id)
        {
            var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryAsync(id));
            if (category == null)
            {
                return NotFound();
            }
            return PartialView(category);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePartial(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            if (!await _categoryRepository.DeleteAsync(category))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
