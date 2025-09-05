using System.Collections.Generic;
using System.Threading.Tasks;
using Inmobiliaria.Models;

namespace Inmobiliaria.Repositories
{
    /// Define operaciones CRUD para Inquilino.
    public interface IInquilinoRepository
    {
        Task<IEnumerable<Inquilino>> GetAllAsync();
        Task<Inquilino?> GetByIdAsync(int id);
        Task<int> CreateAsync(Inquilino i);
        Task<bool> UpdateAsync(Inquilino i);
        Task<bool> LogicalDeleteAsync(int id, string user);
    }
}
