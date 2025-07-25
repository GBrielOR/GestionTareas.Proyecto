using Dapper;
using GestionTareas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace GestionTareas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : Controller
    {
        private DbConnection connection;

        public TareasController(IConfiguration conf)
        {
            var connString = conf.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var tareas = connection.Query<dynamic>("SELECT * FROM Tareas").ToList();
            return tareas;
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var tarea = connection.QueryFirstOrDefault<dynamic>("SELECT * FROM Tareas WHERE Id = @Id", new { Id = id });
            if (tarea == null)
            {
                return NotFound();
            }
            return tarea;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tarea tarea)
        {
            connection.Execute(
                @"INSERT INTO Tareas (Id, Titulo, Descripcion, FechaCreacion, FechaVencimiento, FechaCompletada, UsuarioCreadorId, AsignadoAUsuarioId, ProyectoId, Estado, Prioridad, Progreso)" +
                "VALUES (@Id, @Titulo, @Descripcion, @FechaCreacion, @FechaVencimiento, @FechaCompletada, @UsuarioCreadorId, @AsignadoAUsuarioId, @ProyectoId, @Estado, @Prioridad, @Progreso)",
                new
                {
                    Id = tarea.Id,
                    Titulo = tarea.Titulo,
                    Descripcion = tarea.Descripcion,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaVencimiento = tarea.FechaVencimiento,
                    FechaCompletada = tarea.FechaCompletada,
                    UsuarioCreadorId = tarea.UsuarioCreadorId,
                    AsignadoAUsuarioId = tarea.AsignadoAUsuarioId,
                    ProyectoId = tarea.ProyectoId,
                    Estado = tarea.Estado,
                    Prioridad = tarea.Prioridad,
                    Progreso = tarea.Progreso
                });
            return Ok(tarea);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tarea tarea)
        {
            connection.Execute(
                @"UPDATE Tareas SET Titulo = @Titulo, Descripcion = @Descripcion, FechaVencimiento = @FechaVencimiento, FechaCompletada = @FechaCompletada, UsuarioCreadorId = @UsuarioCreadorId, AsignadoAUsuarioId = @AsignadoAUsuarioId, ProyectoId = @ProyectoId, Estado = @Estado, Prioridad = @Prioridad, Progreso = @Progreso WHERE Id = @Id",
                new
                {
                    Id = id,
                    Titulo = tarea.Titulo,
                    Descripcion = tarea.Descripcion,
                    FechaVencimiento = tarea.FechaVencimiento,
                    FechaCompletada = tarea.FechaCompletada,
                    UsuarioCreadorId = tarea.UsuarioCreadorId,
                    AsignadoAUsuarioId = tarea.AsignadoAUsuarioId,
                    ProyectoId = tarea.ProyectoId,
                    Estado = tarea.Estado,
                    Prioridad = tarea.Prioridad,
                    Progreso = tarea.Progreso
                });
            return Ok(tarea);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Tareas WHERE Id = @Id", new { Id = id });
        }
    }
}
