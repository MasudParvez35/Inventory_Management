﻿using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        public async Task<IList<CategoryModel>> PrepareCategoryListModelAsync(IEnumerable<Category> categories)
        {
            var model = new List<CategoryModel>();

            foreach (var category in categories)
                model.Add(await PrepareCategoryModelAsync(null, category));

            return model;
        }

        public async Task<CategoryModel> PrepareCategoryModelAsync(CategoryModel model, Category category, bool excludeProperties = false)
        {
            if (category != null)
            {
                if (model == null)
                {
                    model = new CategoryModel()
                    {
                        Id = category.Id,
                        Name = category.Name,
                    };
                }
            }

            if (!excludeProperties)
            {
                //model.Name = category.Name;
            }
            return model;
        }
    }
}
