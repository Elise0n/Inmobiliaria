namespace Inmobiliaria.Models
{
    /// Representa a la persona que alquila un inmueble.
    public class Inquilino
    {
        public int Id { get; set; }                 // Clave primaria
        public string Dni { get; set; } = "";       // Documento
        public string Nombre { get; set; } = "";    // Nombres
        public string Apellido { get; set; } = "";  // Apellido(s)
        public string Telefono { get; set; } = "";  // Teléfono de contacto
        public string Email { get; set; } = "";     // Email de contacto
        public string Direccion { get; set; } = ""; // Domicilio
        public bool Activo { get; set; }            // Estado activo (0/1)
        public string? CreadoPor { get; set; }      // Usuario creador
        public DateTime? CreadoEn { get; set; }     // Fecha de creación
        public string? ModificadoPor { get; set; }  // Usuario última modificación
        public DateTime? ModificadoEn { get; set; } // Fecha última modificación
        public bool Eliminado { get; set; }         // Borrado lógico (0/1)
    }
}
