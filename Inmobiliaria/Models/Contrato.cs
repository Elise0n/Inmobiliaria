using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    /// Representa un contrato de alquiler entre un inquilino y un propietario.
    public class Contrato
    {
        public int Id { get; set; } // Clave primaria

        [Required]
        public int InmuebleId { get; set; } // FK a Inmuebles

        [Required]
        public int InquilinoId { get; set; } // FK a Inquilinos

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; } // Fecha de inicio

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; } // Fecha de finalización

        [Required]
        public decimal MontoMensual { get; set; } // Monto mensual

        public decimal? Deposito { get; set; } // Depósito en garantía

        [Required, StringLength(20)]
        public string Estado { get; set; } = "VIGENTE"; // Estado (VIGENTE, FINALIZADO, RESCINDIDO)

        public decimal MultaPct { get; set; } // % de multa en rescisión anticipada

        public string? Observaciones { get; set; } // Notas

        public bool Activo { get; set; } = true;

        public string? CreadoPor { get; set; }
        public DateTime? CreadoEn { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? ModificadoEn { get; set; }
        public bool Eliminado { get; set; }
        public string? EliminadoPor { get; set; }
        public DateTime? EliminadoEn { get; set; }
    }
}
