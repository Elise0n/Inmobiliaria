using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Inmobiliaria.Data;       // Factory de conexión
using Inmobiliaria.Models;
using MySqlConnector;

namespace Inmobiliaria.Repositories
{
    /// Implementa acceso a datos de Inmuebles usando ADO.NET.
    public class InmuebleRepository : IInmuebleRepository
    {
        private readonly DbConnectionFactory _factory;

        public InmuebleRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Inmueble>> GetAllAsync()
        {
            var lista = new List<Inmueble>();
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM inmuebles WHERE eliminado=0 ORDER BY direccion;";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(Map(reader));
            }
            return lista;
        }

        public async Task<Inmueble?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM inmuebles WHERE id=@id AND eliminado=0;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Map(reader);

            return null;
        }

        public async Task<int> CreateAsync(Inmueble i)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO inmuebles
                (propietario_id, direccion, tipo, ambientes, superficie_m2, precio_base, situacion, suspendido_hasta, activo, creado_por, creado_en, eliminado)
                VALUES
                (@propietario_id, @direccion, @tipo, @ambientes, @superficie_m2, @precio_base, @situacion, @suspendido_hasta, @activo, @creado_por, NOW(), 0);
                SELECT LAST_INSERT_ID();";

            cmd.Parameters.Add(new MySqlParameter("@propietario_id", i.PropietarioId));
            cmd.Parameters.Add(new MySqlParameter("@direccion", i.Direccion));
            cmd.Parameters.Add(new MySqlParameter("@tipo", i.Tipo));
            cmd.Parameters.Add(new MySqlParameter("@ambientes", i.Ambientes));
            cmd.Parameters.Add(new MySqlParameter("@superficie_m2", i.SuperficieM2));
            cmd.Parameters.Add(new MySqlParameter("@precio_base", i.PrecioBase));
            cmd.Parameters.Add(new MySqlParameter("@situacion", i.Situacion));
            cmd.Parameters.Add(new MySqlParameter("@suspendido_hasta", i.SuspendidoHasta));
            cmd.Parameters.Add(new MySqlParameter("@activo", i.Activo));
            cmd.Parameters.Add(new MySqlParameter("@creado_por", i.CreadoPor ?? "sistema"));

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(Inmueble i)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE inmuebles SET
                    propietario_id=@propietario_id,
                    direccion=@direccion,
                    tipo=@tipo,
                    ambientes=@ambientes,
                    superficie_m2=@superficie_m2,
                    precio_base=@precio_base,
                    situacion=@situacion,
                    suspendido_hasta=@suspendido_hasta,
                    activo=@activo,
                    modificado_por=@modificado_por,
                    modificado_en=NOW()
                WHERE id=@id AND eliminado=0;";

            cmd.Parameters.Add(new MySqlParameter("@id", i.Id));
            cmd.Parameters.Add(new MySqlParameter("@propietario_id", i.PropietarioId));
            cmd.Parameters.Add(new MySqlParameter("@direccion", i.Direccion));
            cmd.Parameters.Add(new MySqlParameter("@tipo", i.Tipo));
            cmd.Parameters.Add(new MySqlParameter("@ambientes", i.Ambientes));
            cmd.Parameters.Add(new MySqlParameter("@superficie_m2", i.SuperficieM2));
            cmd.Parameters.Add(new MySqlParameter("@precio_base", i.PrecioBase));
            cmd.Parameters.Add(new MySqlParameter("@situacion", i.Situacion));
            cmd.Parameters.Add(new MySqlParameter("@suspendido_hasta", i.SuspendidoHasta));
            cmd.Parameters.Add(new MySqlParameter("@activo", i.Activo));
            cmd.Parameters.Add(new MySqlParameter("@modificado_por", i.ModificadoPor ?? "sistema"));

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> LogicalDeleteAsync(int id, string user)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE inmuebles
                SET eliminado=1, eliminado_por=@user, eliminado_en=NOW()
                WHERE id=@id AND eliminado=0;";

            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.Parameters.Add(new MySqlParameter("@user", user));

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // Mapeo de un registro del reader a objeto Inmueble
        private Inmueble Map(DbDataReader reader)
        {
            return new Inmueble
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                PropietarioId = reader.GetInt32(reader.GetOrdinal("propietario_id")),
                Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                Tipo = reader.GetString(reader.GetOrdinal("tipo")),
                Ambientes = reader.IsDBNull(reader.GetOrdinal("ambientes")) ? null : reader.GetInt32(reader.GetOrdinal("ambientes")),
                SuperficieM2 = reader.IsDBNull(reader.GetOrdinal("superficie_m2")) ? null : reader.GetDecimal(reader.GetOrdinal("superficie_m2")),
                PrecioBase = reader.IsDBNull(reader.GetOrdinal("precio_base")) ? null : reader.GetDecimal(reader.GetOrdinal("precio_base")),
                Situacion = reader.GetString(reader.GetOrdinal("situacion")),
                SuspendidoHasta = reader.IsDBNull(reader.GetOrdinal("suspendido_hasta")) ? null : reader.GetDateTime(reader.GetOrdinal("suspendido_hasta")),
                Activo = reader.GetBoolean(reader.GetOrdinal("activo")),
                CreadoPor = reader.IsDBNull(reader.GetOrdinal("creado_por")) ? null : reader.GetString(reader.GetOrdinal("creado_por")),
                CreadoEn = reader.IsDBNull(reader.GetOrdinal("creado_en")) ? null : reader.GetDateTime(reader.GetOrdinal("creado_en")),
                ModificadoPor = reader.IsDBNull(reader.GetOrdinal("modificado_por")) ? null : reader.GetString(reader.GetOrdinal("modificado_por")),
                ModificadoEn = reader.IsDBNull(reader.GetOrdinal("modificado_en")) ? null : reader.GetDateTime(reader.GetOrdinal("modificado_en")),
                Eliminado = reader.GetBoolean(reader.GetOrdinal("eliminado")),
                EliminadoPor = reader.IsDBNull(reader.GetOrdinal("eliminado_por")) ? null : reader.GetString(reader.GetOrdinal("eliminado_por")),
                EliminadoEn = reader.IsDBNull(reader.GetOrdinal("eliminado_en")) ? null : reader.GetDateTime(reader.GetOrdinal("eliminado_en"))
            };
        }
    }
}
