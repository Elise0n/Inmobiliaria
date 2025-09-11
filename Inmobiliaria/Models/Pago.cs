using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    /// Representa un pago mensual asociado a un contrato.
    public class Pago
    {
        public int Id { get; set; } // Clave primaria

        [Required]
        public int ContratoId { get; set; } // FK a Contratos

        [Required]
        public int NroCuota { get; set; } // Número de cuota (1..n)

        [Required, StringLength(7)]
        public string Periodo { get; set; } = ""; // Periodo (ej: "2025-01")

        [DataType(DataType.Date)]
        public DateTime? FechaPago { get; set; } // Fecha de pago real

        [Required]
        public decimal Importe { get; set; } // Importe fijo

        public decimal Recargo { get; set; } // Recargos

        [Required, StringLength(15)]
        public string Estado { get; set; } = "OK"; // Estado (OK, ANULADO, PENDIENTE)

        public string? CreadoPor { get; set; }
        public DateTime? CreadoEn { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? ModificadoEn { get; set; }
    }
}
