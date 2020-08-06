using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nomad_MVC.Models;

namespace Nomad_MVC.PageHelpers
{
    public static class Helpers
    {
        public static IEnumerable<SelectListItem> PopulateCategoryNameSL(List<Category> categories, object selectedCategory = null)
        {
            var mListItems = categories.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Description
            }).OrderBy(m => m.Text).ToList();
            var baseCategory = new SelectListItem
            {
                Value = null,
                Text = "---select category---"
            };

            mListItems.Insert(0, baseCategory);
            return new SelectList(mListItems, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> PopulateMakeSL(List<Make> makes, object selectedMake = null)
        {
            var mListItems = makes.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Description
            }).OrderBy(m => m.Text).ToList();
            var baseMake = new SelectListItem
            {
                Value = null,
                Text = "---select make---"
            };

            mListItems.Insert(0, baseMake);
            return new SelectList(mListItems, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> PopulateModelSL(List<Model> models, object selectedModel = null)
        {
            var mListItems = models.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Description
            }).OrderBy(m => m.Text).ToList();
            var baseModel = new SelectListItem
            {
                Value = null,
                Text = "---select model---"
            };

            mListItems.Insert(0, baseModel);
            return new SelectList(mListItems, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> PopulateColorSL(List<Color> colors, object selectedColor = null)
        {
            var mListItems = colors.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Description
            }).OrderBy(m => m.Text).ToList();
            var baseColor = new SelectListItem
            {
                Value = null,
                Text = "---select Color---"
            };

            mListItems.Insert(0, baseColor);
            return new SelectList(mListItems, "Value", "Text");
        }
    }
}
