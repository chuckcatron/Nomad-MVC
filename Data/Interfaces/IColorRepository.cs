using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data.Interfaces
{
    public interface IColorRepository: IDisposable
    {
        Task<IEnumerable<Color>> GetColors();
        Task<Color> GetColor(int id);
        Task SaveColor(Color color);
        Task DeleteColor(Color color);
    }
}