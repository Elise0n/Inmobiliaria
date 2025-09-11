using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Inmobiliaria.Data;
using Inmobiliaria.Models;
using MySqlConnector;

namespace Inmobiliaria.Repositories
{
    /// <summary>
    /// Acceso a datos de pagos usando ADO.NET.
    /// </summary>
    public class PagoRepository : IPagoRepository
    {
        private readonly DbConnectionFactory _factory;
        public PagoRepository(DbConnectionFactory factory) => _factory = factory;

        public async Task<IEnumerable<Pago>> GetAllByContratoAsync(int contratoId)
        {
            var lista = new List<Pago>();
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pagos WHERE contrato_id=@cid ORDER BY nro_cuota;";
            cmd.Parameters.Add(new MySqlParameter("@cid", contratoId));

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(Map(reader));

            return lista;
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pagos WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Map(reader);

            return null;
        }

        public async Task<int> CreateAsync(Pago p)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO pagos
                (contrato_id, nro_cuota, periodo, fecha_pago, importe, recargo, estado, creado_por, creado_en)
                VALUES
                (@contrato_id, @nro_cuota, @periodo, @fecha_pago, @importe, @recargo, @estado, @creado_por, NOW());
                SELECT LAST_INSERT_ID();";

            cmd.Parameters.Add(new MySqlParameter("@contrato_id", p.ContratoId));
            cmd.Parameters.Add(new MySqlParameter("@nro_cuota", p.NroCuota));
            cmd.Parameters.Add(new MySqlParameter("@periodo", p.Periodo));
            cmd.Parameters.Add(new MySqlParameter("@fecha_pago", p.FechaPago));
            cmd.Parameters.Add(new MySqlParameter("@importe", p.Importe));
            cmd.Parameters.Add(new MySqlParameter("@recargo", p.Recargo));
            cmd.Parameters.Add(new MySqlParameter("@estado", p.Estado));
            cmd.Parameters.Add(new MySqlParameter("@creado_por", p.CreadoPor ?? "sistema"));

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(Pago p)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            // ⚠️ Solo se permite editar concepto/estado, no monto ni fecha
            cmd.CommandText = @"
                UPDATE pagos SET
                    estado=@estado,
                    modificado_por=@user,
                    modificado_en=NOW()
                WHERE id=@id;";

            cmd.Parameters.Add(new MySqlParameter("@id", p.Id));
            cmd.Parameters.Add(new MySqlParameter("@estado", p.Estado));
            cmd.Parameters.Add(new MySqlParameter("@user", p.ModificadoPor ?? "sistema"));

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> AnularAsync(int id, string user)
        {
            using var conn = _factory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE pagos
                SET estado='ANULADO', anulado_por=@user, anulado_en=NOW()
                WHERE id=@id;";

            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.Parameters.Add(new MySqlParameter("@user", user));

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        private Pago Map(DbDataReader r)
        {
            return new Pago
            {
                Id = r.GetInt32(r.GetOrdinal("id")),
                ContratoId = r.GetInt32(r.GetOrdinal("contrato_id")),
                NroCuota = r.GetInt32(r.GetOrdinal("nro_cuota")),
                Periodo = r.GetString(r.GetOrdinal("periodo")),
                FechaPago = r.IsDBNull(r.GetOrdinal("fecha_pago")) ? null : r.GetDateTime(r.GetOrdinal("fecha_pago")),
                Importe = r.GetDecimal(r.GetOrdinal("importe")),
                Recargo = r.GetDecimal(r.GetOrdinal("recargo")),
                Estado = r.GetString(r.GetOrdinal("estado")),
                CreadoPor = r.IsDBNull(r.GetOrdinal("creado_por")) ? null : r.GetString(r.GetOrdinal("creado_por")),
                CreadoEn = r.IsDBNull(r.GetOrdinal("creado_en")) ? null : r.GetDateTime(r.GetOrdinal("creado_en")),
                ModificadoPor = r.IsDBNull(r.GetOrdinal("modificado_por")) ? null : r.GetString(r.GetOrdinal("modificado_por")),
                ModificadoEn = r.IsDBNull(r.GetOrdinal("modificado_en")) ? null : r.GetDateTime(r.GetOrdinal("modificado_en"))
            };
        }
    }
}
