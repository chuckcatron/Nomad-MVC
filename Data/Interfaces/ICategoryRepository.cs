using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Interfaces
{
    public interface ICategoryRepository : IDisposable
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task SaveCategory(Category category);
        Task DeleteCategory(Category category);
    }
}