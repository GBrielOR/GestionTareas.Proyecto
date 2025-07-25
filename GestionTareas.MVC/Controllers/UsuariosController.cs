using GestionTareas.Api.Models;
using GestionTareas.ApiConsumer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionTareas.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = Crud<Usuario>.GetAll();
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var usuario = Crud<Usuario>.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            CargarUsuarios();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                Crud<Usuario>.Create(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear el usuario: {ex.Message}");
                CargarUsuarios();
                return View(usuario);
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = Crud<Usuario>.GetById(id);
            CargarUsuarios();
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {
            try
            {
                Crud<Usuario>.Update(id, usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al editar el usuario: {ex.Message}");
                CargarUsuarios();
                return View(usuario);
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = Crud<Usuario>.GetById(id);
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario usuario)
        {
            try
            {
                Crud<Usuario>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar el usuario: {ex.Message}");
                CargarUsuarios();
                return View(usuario);
            }
        }

        private void CargarUsuarios()
        {
            var usuarios = Crud<Usuario>.GetAll();
            ViewBag.Usuarios = usuarios.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.NombreDeUsuario // Ajusta el campo según tu modelo Usuario
            }).ToList();
        }
    }
}
