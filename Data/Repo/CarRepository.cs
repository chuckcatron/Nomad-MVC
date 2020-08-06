using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Repo
{
    public class CarRepository : ICarRepository, IDisposable
    {
        private Nomad_MVCContext context { get; }

        public CarRepository(Nomad_MVCContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            var list = await context.Cars
                .Include(car => car.Category)
                .Include(car => car.Make)
                .Include(car => car.Model)
                .Include(car => car.Color)
                .OrderBy(c => c.Make.Description).ThenBy(c => c.Model.Description).ThenByDescending(c => c.Year).ToListAsync();

            foreach (var car in list)
            {
                if (car.Image == null)
                {
                    car.ImageString = "img/place-holder.png";
                }
                else
                {
                    var base64 = Convert.ToBase64String(car.Image);
                    car.ImageString = $"data:image/jpg;base64,{base64}";
                }
            }
            return list;
        }

        public async Task<Car> GetCar(int id)
        {
            var car = await context.Cars
                .Include(car => car.Category)
                .Include(car => car.Make)
                .Include(car => car.Model)
                .Include(car => car.Color)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car.Image == null)
            {
                car.ImageString = "img/place-holder.png";
            }
            else
            {
                var base64 = Convert.ToBase64String(car.Image);
                car.ImageString = $"data:image/jpg;base64,{base64}";
            }

            return car;
        }

        public async Task SaveCar(Car car)
        {
            if (car.Id <= 0)
            {
                context.Cars.Add(car);
                context.Entry(car.Category).State = EntityState.Unchanged;
                context.Entry(car.Make).State = EntityState.Unchanged;
                context.Entry(car.Model).State = EntityState.Unchanged;
                context.Entry(car.Color).State = EntityState.Unchanged;
            }
            else
            {
                context.Cars.Update(car);
                context.Entry(car.Category).State = EntityState.Unchanged;
                context.Entry(car.Make).State = EntityState.Unchanged;
                context.Entry(car.Model).State = EntityState.Unchanged;
                context.Entry(car.Color).State = EntityState.Unchanged;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteCar(Car car)
        {
            context.Cars.Remove(car);
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
