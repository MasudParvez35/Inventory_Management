using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Factories;
using OA_WEB.Models;

namespace OA_WEB.Controllers
{
    public class CategoryController : Controller
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly ICategoryModelFactory _categoryModelFactory;

        #endregion

        #region Ctor

        public CategoryController(ICategoryService categoryService, 
            ICategoryModelFactory categoryModelFactory)
        {
            _categoryService = categoryService;
            _categoryModelFactory = categoryModelFactory;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetAllCategory();
            var model = await _categoryModelFactory.PrepareCategoryListModelAsync(categories);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = await _categoryModelFactory.PrepareCategoryModelAsync(new CategoryModel(), null);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = model.Name,
                };

                await _categoryService.InsertCategoryAsync(category);

                return RedirectToAction("List");
            }

            model = await _categoryModelFactory.PrepareCategoryModelAsync(model, null);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return RedirectToAction("List");

            var model = await _categoryModelFactory.PrepareCategoryModelAsync(null, category);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            var category = await _categoryService.GetCategoryByIdAsync(model.Id);
            if (category == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                category.Name = model.Name;

                await _categoryService.UpdateCategoryAsync(category);

                return RedirectToAction("List");
            }

            model = await _categoryModelFactory.PrepareCategoryModelAsync(model, category);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return RedirectToAction("List");

            var model = await _categoryModelFactory.PrepareCategoryModelAsync(null, category);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryModel model)
        {
            var category = await _categoryService.GetCategoryByIdAsync(model.Id);
            if (category == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                category.Name = model.Name;

                await _categoryService.DeleteCategoryAsync(category);

                return RedirectToAction("List");
            }

            model = await _categoryModelFactory.PrepareCategoryModelAsync(model, category);

            return View(model);
        }

        #endregion
    }
}
