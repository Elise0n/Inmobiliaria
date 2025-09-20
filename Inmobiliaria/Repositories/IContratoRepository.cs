using System.Collections.Generic;
using System.Threading.Tasks;
using Inmobiliaria.Models;

namespace Inmobiliaria.Repositories
{
    /// Define operaciones CRUD para contratos.
    public interface IContratoRepository
    {
        Task<IEnumerable<Contrato>> GetAllAsync();
        Task<Contrato?> GetByIdAsync(int id);
        Task<int> CreateAsync(Contrato c);
        Task<bool> UpdateAsync(Contrato c);
        Task<bool> LogicalDeleteAsync(int id, string user);
        Task<bool> ExisteSuperposicionAsync(int inmuebleId, DateTime inicio, DateTime fin, int? contratoId = null);

    }
}
