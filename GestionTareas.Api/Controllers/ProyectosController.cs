using Dapper;
using GestionTareas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace GestionTareas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : Controller
    {
        private DbConnection connection;

        public ProyectosController(IConfiguration conf)
        {
            var connString = conf.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var proyectos = connection.Query<dynamic>("SELECT * FROM Proyectos").ToList();
            return proyectos;
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var proyecto = connection.QueryFirstOrDefault<dynamic>("SELECT * FROM Proyectos WHERE Id = @Id", new { Id = id });
            if (proyecto == null)
            {
                return NotFound();
            }
            return proyecto;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Proyecto proyecto)
        {
            connection.Execute(
                @"INSERT INTO Proyectos (Id, Nombre, Descripcion, FechaCreacion, FechaInicio, FechaFin, UsuarioCreadorId, Estado)" +
                "VALUES (@Id, @Nombre, @Descripcion, @FechaCreacion, @FechaInicio, @FechaFin, @UsuarioCreadorId, @Estado)",
                new
                {
                    Id = proyecto.Id,
                    Nombre = proyecto.Nombre,
                    Descripcion = proyecto.Descripcion,
                    FechaCreacion = proyecto.FechaCreacion,
                    FechaInicio = proyecto.FechaInicio,
                    FechaFin = proyecto.FechaFin,
                    UsuarioCreadorId = proyecto.UsuarioCreadorId,
                    Estado = proyecto.Estado
                });
            return Ok(proyecto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Proyecto proyecto)
        {
            var existingProyecto = connection.QueryFirstOrDefault<dynamic>("SELECT * FROM Proyectos WHERE Id = @Id", new { Id = id });
            if (existingProyecto == null)
            {
                return NotFound();
            }
            connection.Execute(
                @"UPDATE Proyectos SET Nombre = @Nombre, Descripcion = @Descripcion, FechaInicio = @FechaInicio, FechaFin = @FechaFin, UsuarioCreadorId = @UsuarioCreadorId, Estado = @Estado WHERE Id = @Id",
                new
                {
                    Id = id,
                    Nombre = proyecto.Nombre,
                    Descripcion = proyecto.Descripcion,
                    FechaInicio = proyecto.FechaInicio,
                    FechaFin = proyecto.FechaFin,
                    UsuarioCreadorId = proyecto.UsuarioCreadorId,
                    Estado = proyecto.Estado
                });
            return Ok(proyecto);
        }

        [HttpDelete("{id}")]
        public dynamic Delete(int id)
        {
            var existingProyecto = connection.QueryFirstOrDefault<dynamic>("SELECT * FROM Proyectos WHERE Id = @Id", new { Id = id });
            if (existingProyecto == null)
            {
                return NotFound();
            }
            connection.Execute("DELETE FROM Proyectos WHERE Id = @Id", new { Id = id });
            return Ok(new { Message = "Proyecto eliminado correctamente." });
        }
    }
}
