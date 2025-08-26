
using Domain.Data;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repository.IRepository;
using Domain.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;




namespace Admin.Areas.AdminV.Controllers
{

    [Area("AdminV")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductService productService, ICategoryRepository categoryRepository)
        {
            _productService = productService;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }


        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View(new ProductDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(dto);
            }
            await _productService.CreateProductAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _productService.GetProductByIdAsync(id);
            if (dto is null) return NotFound();
            await LoadCategoriesAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(dto);
            }
            await _productService.EditProductAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _productService.GetProductByIdAsync(id);
            if (dto is null) return NotFound();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task LoadCategoriesAsync()
        {
            var cats = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryList = cats.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),  
                Text = c.Name
            }).ToList();
        }
    }
}


