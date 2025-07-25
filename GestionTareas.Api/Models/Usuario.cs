using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTareas.Api.Models
{

    [Table("Usuarios")]

    public class Usuario
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool Activo {  get; set; } = true;

    }
}
