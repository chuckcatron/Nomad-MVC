using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Interfaces
{
    public interface ICarRepository : IDisposable
    {
        Task<IEnumerable<Car>> GetCars();
        Task<Car> GetCar(int id);
        Task SaveCar(Car car);
        Task DeleteCar(Car car);
    }
}