using System.Collections.Generic;
using System.Threading.Tasks;
using Inmobiliaria.Models;

namespace Inmobiliaria.Repositories
{
    /// Define operaciones CRUD para Propietario.
    public interface IPropietarioRepository
    {
        Task<IEnumerable<Propietario>> GetAllAsync();      // Lista (no eliminados)
        Task<Propietario?> GetByIdAsync(int id);           // Detalle por Id
        Task<int> CreateAsync(Propietario p);              // Alta (retorna Id nuevo)
        Task<bool> UpdateAsync(Propietario p);             // Modificación
        Task<bool> LogicalDeleteAsync(int id, string user);// Borrado lógico (Eliminado=1)
    }
}
