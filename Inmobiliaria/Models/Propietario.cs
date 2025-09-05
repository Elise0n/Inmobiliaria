namespace Inmobiliaria.Models
{
    /// Representa al dueño de uno o varios inmuebles.
    public class Propietario
    {
        public int Id { get; set; }                 // Clave primaria (int, autoincremental en BD)
        public string Dni { get; set; } = "";       // Documento de identidad (varchar)
        public string Nombre { get; set; } = "";    // Nombres del propietario
        public string Apellido { get; set; } = "";  // Apellido(s) del propietario
        public string Telefono { get; set; } = "";  // Teléfono de contacto
        public string Email { get; set; } = "";     // Email de contacto
        public string Direccion { get; set; } = ""; // Domicilio
        public bool Activo { get; set; }            // Indica si está activo (tinyint 0/1)
        public string? CreadoPor { get; set; }      // Usuario que creó el registro
        public DateTime? CreadoEn { get; set; }     // Fecha/hora de creación
        public string? ModificadoPor { get; set; }  // Usuario que modificó por última vez
        public DateTime? ModificadoEn { get; set; } // Fecha/hora de la última modificación
        public bool Eliminado { get; set; }         // Borrado lógico (tinyint 0/1)
    }
}
