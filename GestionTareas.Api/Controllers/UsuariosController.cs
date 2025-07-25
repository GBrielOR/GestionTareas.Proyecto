using Dapper;
using GestionTareas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace GestionTareas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private DbConnection connection;

        public UsuariosController(IConfiguration conf)
        {
            var connString = conf.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();

        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var usuarios = connection.Query<dynamic>("SELECT * FROM Usuarios").ToList();
            return usuarios;
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var usuario = connection.QueryFirstOrDefault<dynamic>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            connection.Execute(
                @"INSERT INTO Usuarios (id, NombreDeUsuario, Email, PasswordHash, NombreCompleto, FechaCreacion, Activo)" +
                "VALUES (@Id, @NombreDeUsuario, @Email, @PasswordHash, @NombreCompleto, @FechaCreacion, @Activo)",
                new
                {
                    Id = usuario.Id,
                    NombreDeUsuario = usuario.NombreDeUsuario,
                    Email = usuario.Email,
                    PasswordHash = usuario.PasswordHash,
                    NombreCompleto = usuario.NombreCompleto,
                    FechaCreacion = usuario.FechaCreacion,
                    Activo = usuario.Activo
                });
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            connection.Execute(
                "UPDATE Usuarios SET NombreDeUsuario = @NombreDeUsuario, Email = @Email, PasswordHash = @PasswordHash, NombreCompleto = @NombreCompleto, FechaCreacion = @FechaCreacion, Activo = @Activo WHERE Id = @Id",
                new {
                  usuario.Id,
                  usuario.NombreDeUsuario,
                  usuario.Email,
                  usuario.PasswordHash,
                  usuario.NombreCompleto,
                  usuario.FechaCreacion,
                  usuario.Activo
                });
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
