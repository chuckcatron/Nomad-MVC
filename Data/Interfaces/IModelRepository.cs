using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Interfaces
{
    public interface IModelRepository : IDisposable
    {
        Task<IEnumerable<Model>> GetModels();
        Task<Model> GetModel(int id);
        Task SaveModel(Model model);
        Task DeleteModel(Model model);
    }
}