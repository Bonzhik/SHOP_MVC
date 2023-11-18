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
                TempData["error"] = "This item already exists";
                return RedirectToAction("Index");
            }
            if (!await _categoryRepository.AddAsync(category))
            {
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Success";
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
                TempData["error"] = "This item already exists";
                return RedirectToAction("Index");
            }
            if (!await _categoryRepository.UpdateAsync(category))
            {
                TempData["error"] = "Something went wrong while saving";
                return View("Error");
            }
            TempData["success"] = "Success";
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
                TempData["error"] = "Something went wrong while saving";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Success";
            return RedirectToAction("Index");
        }
    }
}
