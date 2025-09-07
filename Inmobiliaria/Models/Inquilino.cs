using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(20, ErrorMessage = "El DNI no puede superar 20 caracteres.")]
        public string Dni { get; set; } = "";

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Apellido { get; set; } = "";

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [RegularExpression(@"^[0-9+\-\s()]*$", ErrorMessage = "Formato de teléfono inválido.")]
        public string Telefono { get; set; } = "";

        [EmailAddress(ErrorMessage = "Email inválido.")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres.")]
        public string Email { get; set; } = "";

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Direccion { get; set; } = "";

        public bool Activo { get; set; }

        public string? CreadoPor { get; set; }
        public DateTime? CreadoEn { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? ModificadoEn { get; set; }
        public bool Eliminado { get; set; }
    }
}
