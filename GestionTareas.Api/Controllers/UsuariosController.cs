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
        public dynamic Post([FromBody] dynamic usuario)
        {
            connection.Execute(
                @"INSERT INTO Usuarios (id, NombreUsuario, Email, PasswordHash, NombreCompleto, FechaCreacion, Activo)" +
                "VALUES (@Id, @NombreUsuario, @Email, @PasswordHash, @NombreCompleto, @FechaCreacion, @Activo)",
                new
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario,
                    Email = usuario.Email,
                    PasswordHash = usuario.PasswordHash,
                    NombreCompleto = usuario.NombreCompleto,
                    FechaCreacion = usuario.FechaCreacion,
                    Activo = usuario.Activo
                });
            return usuario;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            connection.Execute(
                "UPDATE Usuarios SET NombreUsuario = @NombreUsuario, Email = @Email, PasswordHash = @PasswordHash, NombreCompleto = @NombreCompleto, FechaCreacion = @FechaCreacion, Activo = @Activo WHERE Id = @Id",
                new {
                  usuario.Id,
                  usuario.NombreUsuario,
                  usuario.Email,
                  usuario.PasswordHash,
                  usuario.NombreCompleto,
                  usuario.FechaCreacion,
                  usuario.Activo
                });
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
