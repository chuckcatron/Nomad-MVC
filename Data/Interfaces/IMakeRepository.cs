using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Interfaces
{
    public interface IMakeRepository : IDisposable
    {
        Task<IEnumerable<Make>> GetMakes();
        Task<Make> GetMake(int id);
        Task SaveMake(Make make);
        Task DeleteMake(Make make);
    }
}