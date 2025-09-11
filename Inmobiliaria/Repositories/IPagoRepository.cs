using System.Collections.Generic;
using System.Threading.Tasks;
using Inmobiliaria.Models;

namespace Inmobiliaria.Repositories
{
    /// Define operaciones CRUD para pagos.
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAllByContratoAsync(int contratoId); // lista por contrato
        Task<Pago?> GetByIdAsync(int id);
        Task<int> CreateAsync(Pago p);
        Task<bool> UpdateAsync(Pago p);              // solo se permite editar concepto
        Task<bool> AnularAsync(int id, string user); // eliminación lógica
    }
}
