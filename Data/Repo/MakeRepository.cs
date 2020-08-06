using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Repo
{
    public class MakeRepository : IMakeRepository, IDisposable
    {
        private Nomad_MVCContext context { get; }

        public MakeRepository(Nomad_MVCContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Make>> GetMakes()
        {
            var list = await context.Makes.OrderBy(c => c.Description).ToListAsync();
            return list;
        }

        public async Task<Make> GetMake(int id)
        {
            var make = await context.Makes.FindAsync(id);
            return make;
        }

        public async Task SaveMake(Make make)
        {
            if (make.Id <= 0)
            {
                context.Makes.Add(make);
            }
            else
            {
                var updatedMake = context.Makes.FindAsync(make.Id).Result;
                updatedMake.Description = make.Description;
                context.Makes.Update(updatedMake);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteMake(Make make)
        {
            context.Makes.Remove(make);
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
