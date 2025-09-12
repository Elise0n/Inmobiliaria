using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Inmobiliaria.Data;     // Para DbConnectionFactory
using Inmobiliaria.Models;  // Para Propietario
using MySql.Data.MySqlClient;          // Para parámetros MySql
using System.Data.Common;

namespace Inmobiliaria.Repositories
{
    /// Implementación ADO.NET de IInquilinoRepository (MySQL).
    public class InquilinoRepository(DbConnectionFactory factory) : IInquilinoRepository
    {
        private readonly DbConnectionFactory _factory = factory;    // Factory de conexiones

        public async Task<IEnumerable<Inquilino>> GetAllAsync()
        {
            var list = new List<Inquilino>();        // Donde acumulamos resultados

            using var conn = _factory.CreateOpenConnection(); // Abrimos conexión
            using var cmd = conn.CreateCommand();              // Creamos comando SQL

            // Consulta sólo registros no eliminados
            cmd.CommandText = @"
                SELECT id, dni, nombre, apellido, telefono, email, direccion,
                       activo, creado_por, creado_en, modificado_por, modificado_en, eliminado
                FROM inquilinos
                WHERE eliminado = 0
                ORDER BY apellido, nombre;";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())                   // Iteramos cada fila
            {
                // Mapear columnas a objeto Inquilino
                list.Add(new Inquilino
                {
                    Id = reader.GetInt32("id"),
                    Dni = reader.GetString("dni"),
                    Nombre = reader.GetString("nombre"),
                    Apellido = reader.GetString("apellido"),
                    Telefono = reader.GetString("telefono"),
                    Email = reader.GetString("email"),
                    Direccion = reader.GetString("direccion"),
                    Activo = reader.GetBoolean("activo"),
                    CreadoPor = reader.IsDBNull("creado_por") ? null : reader.GetString("creado_por"),
                    CreadoEn = reader.IsDBNull("creado_en") ? null : reader.GetDateTime("creado_en"),
                    ModificadoPor = reader.IsDBNull("modificado_por") ? null : reader.GetString("modificado_por"),
                    ModificadoEn = reader.IsDBNull("modificado_en") ? null : reader.GetDateTime("modificado_en"),
                    Eliminado = reader.GetBoolean("eliminado")
                });
            }
            return list; // Retornamos la lista
        }

        public async Task<Inquilino?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT id, dni, nombre, apellido, telefono, email, direccion,
                       activo, creado_por, creado_en, modificado_por, modificado_en, eliminado
                FROM inquilinos
                WHERE id = @id AND eliminado = 0;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Inquilino
                {
                    Id = reader.GetInt32("id"),
                    Dni = reader.GetString("dni"),
                    Nombre = reader.GetString("nombre"),
                    Apellido = reader.GetString("apellido"),
                    Telefono = reader.GetString("telefono"),
                    Email = reader.GetString("email"),
                    Direccion = reader.GetString("direccion"),
                    Activo = reader.GetBoolean("activo"),
                    CreadoPor = reader.IsDBNull("creado_por") ? null : reader.GetString("creado_por"),
                    CreadoEn = reader.IsDBNull("creado_en") ? null : reader.GetDateTime("creado_en"),
                    ModificadoPor = reader.IsDBNull("modificado_por") ? null : reader.GetString("modificado_por"),
                    ModificadoEn = reader.IsDBNull("modificado_en") ? null : reader.GetDateTime("modificado_en"),
                    Eliminado = reader.GetBoolean("eliminado")
                };
            }
            return null; // No encontrado
        }

        public async Task<int> CreateAsync(Inquilino i)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO inquilinos
                (dni, nombre, apellido, telefono, email, direccion, activo,
                 creado_por, creado_en, eliminado)
                VALUES
                (@dni, @nombre, @apellido, @telefono, @email, @direccion, @activo,
                 @creado_por, NOW(), 0);
                SELECT LAST_INSERT_ID();";
            cmd.Parameters.Add(new MySqlParameter("@dni", i.Dni));
            cmd.Parameters.Add(new MySqlParameter("@nombre", i.Nombre));
            cmd.Parameters.Add(new MySqlParameter("@apellido", i.Apellido));
            cmd.Parameters.Add(new MySqlParameter("@telefono", i.Telefono));
            cmd.Parameters.Add(new MySqlParameter("@email", i.Email));
            cmd.Parameters.Add(new MySqlParameter("@direccion", i.Direccion));
            cmd.Parameters.Add(new MySqlParameter("@activo", i.Activo));
            cmd.Parameters.Add(new MySqlParameter("@creado_por", i.CreadoPor ?? "sistema"));

            var result = await cmd.ExecuteScalarAsync();       // Devuelve el ID nuevo
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(Inquilino i)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE inquilinos SET
                    dni=@dni, nombre=@nombre, apellido=@apellido, telefono=@telefono,
                    email=@email, direccion=@direccion, activo=@activo,
                    modificado_por=@modificado_por, modificado_en=NOW()
                WHERE id=@id AND eliminado=0;";
            cmd.Parameters.Add(new MySqlParameter("@dni", i.Dni));
            cmd.Parameters.Add(new MySqlParameter("@nombre", i.Nombre));
            cmd.Parameters.Add(new MySqlParameter("@apellido", i.Apellido));
            cmd.Parameters.Add(new MySqlParameter("@telefono", i.Telefono));
            cmd.Parameters.Add(new MySqlParameter("@email", i.Email));
            cmd.Parameters.Add(new MySqlParameter("@direccion", i.Direccion));
            cmd.Parameters.Add(new MySqlParameter("@activo", i.Activo));
            cmd.Parameters.Add(new MySqlParameter("@modificado_por", i.ModificadoPor ?? "sistema"));
            cmd.Parameters.Add(new MySqlParameter("@id", i.Id));

            var rows = await cmd.ExecuteNonQueryAsync();// Filas afectadas
            return rows > 0;
        }

        public async Task<bool> LogicalDeleteAsync(int id, string user)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE inquilinos SET
                    eliminado=1, modificado_por=@user, modificado_en=NOW()
                WHERE id=@id AND eliminado=0;";
            cmd.Parameters.Add(new MySqlParameter("@user", user));
            cmd.Parameters.Add(new MySqlParameter("@id", id));

            var rows = await cmd.ExecuteNonQueryAsync();

            return rows > 0;
        }
    }
}
