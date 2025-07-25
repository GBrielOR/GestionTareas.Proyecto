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
    public class TareasController : Controller
    {
        // GET: Tareas
        public ActionResult Index()
        {
            var tareas = Crud<Tarea>.GetAll();
            return View(tareas);
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            CargarTareas();
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Create(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear la tarea: {ex.Message}");
                CargarTareas();
                return View(tarea);
            }
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            CargarTareas();
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Update(id, tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al editar la tarea: {ex.Message}");
                CargarTareas();
                return View(tarea);
            }
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar la tarea: {ex.Message}");
                CargarTareas();
                return View(tarea);
            }
        }

        private void CargarTareas()
        {
            var tareas = Crud<Tarea>.GetAll();
            ViewBag.Tareas = tareas.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Titulo
            }).ToList();
        }
    }
}
