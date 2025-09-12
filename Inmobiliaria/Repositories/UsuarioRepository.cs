using Inmobiliaria.Data;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Repositories
{
    public class UsuarioRepository(DbConnectionFactory factory) : IUsuarioRepository
    {
        private readonly DbConnectionFactory _factory = factory;

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, email, password_hash, rol, nombre FROM usuarios WHERE email=@e";
            cmd.Parameters.Add(new MySqlParameter("@e", email));
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    Rol = reader.GetString(3),
                    Nombre = reader.IsDBNull(4) ? "" : reader.GetString(4)
                };
            }
            return null;
        }

        public async Task<int> CreateAsync(Usuario u)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO usuarios (email, password_hash, rol, nombre)
                VALUES (@e, @p, @r, @n);
                SELECT LAST_INSERT_ID();";
            cmd.Parameters.Add(new MySqlParameter("@e", u.Email));
            cmd.Parameters.Add(new MySqlParameter("@p", u.PasswordHash));
            cmd.Parameters.Add(new MySqlParameter("@r", u.Rol));
            cmd.Parameters.Add(new MySqlParameter("@n", u.Nombre ?? ""));
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }
}
