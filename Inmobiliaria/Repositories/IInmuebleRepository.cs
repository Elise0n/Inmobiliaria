using System.Collections.Generic;
using System.Threading.Tasks;
using Inmobiliaria.Models;

namespace Inmobiliaria.Repositories
{
    /// Define las operaciones CRUD (ABM) para Inmuebles.
    public interface IInmuebleRepository
    {
        Task<IEnumerable<Inmueble>> GetAllAsync();
        Task<Inmueble?> GetByIdAsync(int id);
        Task<int> CreateAsync(Inmueble i);
        Task<bool> UpdateAsync(Inmueble i);
        Task<bool> LogicalDeleteAsync(int id, string user);
    }
}
