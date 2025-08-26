using Domain.Data;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repository.IRepository;
using Domain.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Areas.AdminV.Controllers
{
    [Area("AdminV")]
    [Authorize(Roles = "SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _categoryService.GetAllAsync();
            return View(list); 
        }

        public IActionResult Create() => View(new CategoryDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
            if (dto.Name == dto.DisplayOrder.ToString())
            {
                ModelState.AddModelError(nameof(dto.Name), "The DisplayOrder cannot exactly match the Name.");
            }

            if (!ModelState.IsValid) return View(dto);

            await _categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _categoryService.EditAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return View(dto); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
