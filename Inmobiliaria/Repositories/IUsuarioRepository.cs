using Inmobiliaria.Models;
using System.Threading.Tasks;

namespace Inmobiliaria.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmailAsync(string email);
        Task<int> CreateAsync(Usuario u);
    }
}
