using System;                           // Provee tipos básicos (Exception, etc.)
using System.Data;                       // Contiene interfaces ADO.NET (IDbConnection)
using Microsoft.Extensions.Configuration;// Permite leer configuración (appsettings.json)
using System.Data.Common;
using MySql.Data.MySqlClient;               // Contiene DbConnection

namespace Inmobiliaria.Data           // Namespace del proyecto (ajusta si tu proyecto cambia)
{
    /// <summary>
    /// Crea conexiones IDbConnection para MySQL usando la cadena de conexión "DefaultConnection".
    /// </summary>
    public class DbConnectionFactory
    {
        private readonly string _connectionString; // Guarda la cadena de conexión leída del appsettings

        // El constructor recibe IConfiguration inyectado por el contenedor de DI
        public DbConnectionFactory(IConfiguration configuration)
        {
            // Obtiene la cadena "DefaultConnection" de appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Falta ConnectionStrings:DefaultConnection");
        }

        /// Retorna una conexión abierta a MySQL (IDbConnection).
        /// El caller debe disponerla (using) cuando termina.
        public DbConnection CreateOpenConnection()
        {
            // Crea una conexión MySqlConnection usando la cadena
            var conn = new MySqlConnection(_connectionString);
            // Abre la conexión para que esté lista para ejecutar comandos
            conn.Open();
            // Retorna la conexión ya abierta
            return conn;
        }
    }
}
