using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool Activo {  get; set; } = true;

    }
}
