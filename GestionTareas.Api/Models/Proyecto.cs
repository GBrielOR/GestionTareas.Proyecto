using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Api.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int UsuarioCreadorId { get; set; } 
        public string Estado { get; set; }

    }
}
