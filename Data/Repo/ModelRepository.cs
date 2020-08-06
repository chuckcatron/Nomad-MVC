using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Repo
{
    public class ModelRepository : IModelRepository, IDisposable
    {
        private Nomad_MVCContext context { get; }

        public ModelRepository(Nomad_MVCContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Model>> GetModels()
        {
            var list = await context.Models.OrderBy(c => c.Description).ToListAsync();
            return list;
        }

        public async Task<Model> GetModel(int id)
        {
            var model = await context.Models.FindAsync(id);
            return model;
        }

        public async Task SaveModel(Model model)
        {
            if (model.Id <= 0)
            {
                context.Models.Add(model);
            }
            else
            {
                var updatedModel = context.Models.FindAsync(model.Id).Result;
                updatedModel.Description = model.Description;
                context.Models.Update(updatedModel);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteModel(Model model)
        {
            context.Models.Remove(model);
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
