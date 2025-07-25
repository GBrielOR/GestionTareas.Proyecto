using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Api.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaCompletada { get; set; }
        public int UsuarioCreadorId { get; set; } 
        public int UsuarioAsignadoId { get; set; }
        public int ProyectoId { get; set; } 
        public string EstadoTarea { get; set; }
        public string PrioridadTarea { get; set; }
        public int Progreso { get; set; } = 0;
    }
}
