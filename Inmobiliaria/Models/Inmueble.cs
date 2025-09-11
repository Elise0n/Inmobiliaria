using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    /// Representa un inmueble (propiedad) ofrecido en alquiler.
    public class Inmueble
    {
        public int Id { get; set; }  // Clave primaria

        [Required(ErrorMessage = "El propietario es obligatorio.")]
        public int PropietarioId { get; set; }  // FK a Propietarios

        [Required, StringLength(200)]
        public string Direccion { get; set; } = ""; // Dirección completa

        [Required, StringLength(50)]
        public string Tipo { get; set; } = ""; // Casa, Departamento, Local, etc.

        public int? Ambientes { get; set; } // Cantidad de ambientes

        public decimal? SuperficieM2 { get; set; } // Superficie en metros cuadrados

        [Required]
        public decimal? PrecioBase { get; set; } // Precio de referencia

        [Required]
        [StringLength(20)]
        public string Situacion { get; set; } = "DISPONIBLE"; // Estado (DISPONIBLE, ALQUILADO, SUSPENDIDO)

        public DateTime? SuspendidoHasta { get; set; } // Fecha hasta la que se suspende oferta

        public bool Activo { get; set; } = true; // Activo o no

        public string? CreadoPor { get; set; }
        public DateTime? CreadoEn { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? ModificadoEn { get; set; }
        public bool Eliminado { get; set; }
        public string? EliminadoPor { get; set; }
        public DateTime? EliminadoEn { get; set; }
    }
}
