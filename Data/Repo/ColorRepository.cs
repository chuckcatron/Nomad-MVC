using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Repo
{
    public class ColorRepository : IColorRepository, IDisposable
    {
        private Nomad_MVCContext context { get; }

        public ColorRepository(Nomad_MVCContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            var colorList = await context.Colors.OrderBy(c => c.Description).ToListAsync();
            return colorList;
        }

        public async Task<Color> GetColor(int id)
        {
            var color = await context.Colors.FindAsync(id);
            return color;
        }

        public async Task SaveColor(Color color)
        {
            if (color.Id <= 0)
            {
                context.Colors.Add(color);
            }
            else
            {
                var updatedColor = context.Colors.FindAsync(color.Id).Result;
                updatedColor.Description = color.Description;
                context.Colors.Update(updatedColor);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteColor(Color color)
        {
            context.Colors.Remove(color);
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
