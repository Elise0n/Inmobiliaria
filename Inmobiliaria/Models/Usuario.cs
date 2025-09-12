using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    /// Representa un usuario del sistema (empleado o administrador).
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; } // Guardamos hash, no la contraseña en claro

        [Required]
        public string? Rol { get; set; } // "Administrador" o "Empleado"

        public string? Nombre { get; set; }
    }
}
