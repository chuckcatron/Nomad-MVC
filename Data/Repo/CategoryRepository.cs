using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Repo
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private Nomad_MVCContext context { get; }

        public CategoryRepository(Nomad_MVCContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var list = await context.Categories.OrderBy(c => c.Description).ToListAsync();
            return list;
        }

        public async Task<Category> GetCategory(int id)
        {
            var cat = await context.Categories.FindAsync(id);
            return cat;
        }

        public async Task SaveCategory(Category category)
        {
            if (category.Id <= 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                var updatedCategory = context.Categories.FindAsync(category.Id).Result;
                updatedCategory.Description = category.Description;
                context.Categories.Update(updatedCategory);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
