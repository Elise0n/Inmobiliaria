using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Inmobiliaria.Data;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Repositories
{
    /// <summary>
    /// Acceso a datos de contratos usando ADO.NET.
    /// </summary>
    public class ContratoRepository(DbConnectionFactory factory) : IContratoRepository
    {
        private readonly DbConnectionFactory _factory = factory;

        public async Task<IEnumerable<Contrato>> GetAllAsync()
        {
            var lista = new List<Contrato>();
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM contratos WHERE eliminado=0 ORDER BY fecha_inicio DESC;";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(Map(reader));

            return lista;
        }

        public async Task<Contrato?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM contratos WHERE id=@id AND eliminado=0;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Map(reader);

            return null;
        }

        public async Task<int> CreateAsync(Contrato c)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO contratos
                (inmueble_id, inquilino_id, fecha_inicio, fecha_fin, monto_mensual, deposito, estado, multa_pct, observaciones, activo, creado_por, creado_en, eliminado)
                VALUES
                (@inmueble_id, @inquilino_id, @fecha_inicio, @fecha_fin, @monto_mensual, @deposito, @estado, @multa_pct, @observaciones, @activo, @creado_por, NOW(), 0);
                SELECT LAST_INSERT_ID();";

            cmd.Parameters.Add(new MySqlParameter("@inmueble_id", c.InmuebleId));
            cmd.Parameters.Add(new MySqlParameter("@inquilino_id", c.InquilinoId));
            cmd.Parameters.Add(new MySqlParameter("@fecha_inicio", c.FechaInicio));
            cmd.Parameters.Add(new MySqlParameter("@fecha_fin", c.FechaFin));
            cmd.Parameters.Add(new MySqlParameter("@monto_mensual", c.MontoMensual));
            cmd.Parameters.Add(new MySqlParameter("@deposito", c.Deposito));
            cmd.Parameters.Add(new MySqlParameter("@estado", c.Estado));
            cmd.Parameters.Add(new MySqlParameter("@multa_pct", c.MultaPct));
            cmd.Parameters.Add(new MySqlParameter("@observaciones", c.Observaciones));
            cmd.Parameters.Add(new MySqlParameter("@activo", c.Activo));
            cmd.Parameters.Add(new MySqlParameter("@creado_por", c.CreadoPor ?? "sistema"));

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(Contrato c)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE contratos SET
                    inmueble_id=@inmueble_id,
                    inquilino_id=@inquilino_id,
                    fecha_inicio=@fecha_inicio,
                    fecha_fin=@fecha_fin,
                    monto_mensual=@monto_mensual,
                    deposito=@deposito,
                    estado=@estado,
                    multa_pct=@multa_pct,
                    observaciones=@observaciones,
                    activo=@activo,
                    modificado_por=@modificado_por,
                    modificado_en=NOW()
                WHERE id=@id AND eliminado=0;";

            cmd.Parameters.Add(new MySqlParameter("@id", c.Id));
            cmd.Parameters.Add(new MySqlParameter("@inmueble_id", c.InmuebleId));
            cmd.Parameters.Add(new MySqlParameter("@inquilino_id", c.InquilinoId));
            cmd.Parameters.Add(new MySqlParameter("@fecha_inicio", c.FechaInicio));
            cmd.Parameters.Add(new MySqlParameter("@fecha_fin", c.FechaFin));
            cmd.Parameters.Add(new MySqlParameter("@monto_mensual", c.MontoMensual));
            cmd.Parameters.Add(new MySqlParameter("@deposito", c.Deposito));
            cmd.Parameters.Add(new MySqlParameter("@estado", c.Estado));
            cmd.Parameters.Add(new MySqlParameter("@multa_pct", c.MultaPct));
            cmd.Parameters.Add(new MySqlParameter("@observaciones", c.Observaciones));
            cmd.Parameters.Add(new MySqlParameter("@activo", c.Activo));
            cmd.Parameters.Add(new MySqlParameter("@modificado_por", c.ModificadoPor ?? "sistema"));

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> LogicalDeleteAsync(int id, string user)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE contratos SET eliminado=1, eliminado_por=@user, eliminado_en=NOW()
                WHERE id=@id AND eliminado=0;";

            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.Parameters.Add(new MySqlParameter("@user", user));

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        private Contrato Map(DbDataReader r)
        {
            return new Contrato
            {
                Id = r.GetInt32(r.GetOrdinal("id")),
                InmuebleId = r.GetInt32(r.GetOrdinal("inmueble_id")),
                InquilinoId = r.GetInt32(r.GetOrdinal("inquilino_id")),
                FechaInicio = r.GetDateTime(r.GetOrdinal("fecha_inicio")),
                FechaFin = r.GetDateTime(r.GetOrdinal("fecha_fin")),
                MontoMensual = r.GetDecimal(r.GetOrdinal("monto_mensual")),
                Deposito = r.IsDBNull(r.GetOrdinal("deposito")) ? null : r.GetDecimal(r.GetOrdinal("deposito")),
                Estado = r.GetString(r.GetOrdinal("estado")),
                MultaPct = r.GetDecimal(r.GetOrdinal("multa_pct")),
                Observaciones = r.IsDBNull(r.GetOrdinal("observaciones")) ? null : r.GetString(r.GetOrdinal("observaciones")),
                Activo = r.GetBoolean(r.GetOrdinal("activo")),
                CreadoPor = r.IsDBNull(r.GetOrdinal("creado_por")) ? null : r.GetString(r.GetOrdinal("creado_por")),
                CreadoEn = r.IsDBNull(r.GetOrdinal("creado_en")) ? null : r.GetDateTime(r.GetOrdinal("creado_en")),
                ModificadoPor = r.IsDBNull(r.GetOrdinal("modificado_por")) ? null : r.GetString(r.GetOrdinal("modificado_por")),
                ModificadoEn = r.IsDBNull(r.GetOrdinal("modificado_en")) ? null : r.GetDateTime(r.GetOrdinal("modificado_en")),
                Eliminado = r.GetBoolean(r.GetOrdinal("eliminado"))
            };
        }
    }
}
