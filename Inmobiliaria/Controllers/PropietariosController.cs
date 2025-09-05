using System.Threading.Tasks;                         // Para Task/async
using Microsoft.AspNetCore.Mvc;                      // Base MVC
using Inmobiliaria.Repositories;                  // Repositorios
using Inmobiliaria.Models;                        // Modelos

namespace Inmobiliaria.Controllers
{
    /// Controlador MVC para ABM de Propietarios.
    public class PropietariosController : Controller
    {
        private readonly IPropietarioRepository _repo; // Inyección del repo

        public PropietariosController(IPropietarioRepository repo)
        {
            _repo = repo;                              // Guardamos el repo
        }

        // GET: /Propietarios
        public async Task<IActionResult> Index()
        {
            var lista = await _repo.GetAllAsync();     // Obtenemos todos
            return View(lista);                        // Pasamos la lista a la vista
        }

        // GET: /Propietarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _repo.GetByIdAsync(id);   // Buscamos por Id
            if (item == null) return NotFound();       // 404 si no existe
            return View(item);                         // Mostramos la vista
        }

        // GET: /Propietarios/Create
        public IActionResult Create() => View();       // Devuelve formulario vacío

        // POST: /Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Propietario p)
        {
            if (!ModelState.IsValid) return View(p);   // Valida modelo
            p.CreadoPor = User?.Identity?.Name ?? "sistema"; // Auditoría simple
            var id = await _repo.CreateAsync(p);       // Inserta en BD
            return RedirectToAction(nameof(Details), new { id }); // Redirige a Details
        }

        // GET: /Propietarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Propietario p)
        {
            if (id != p.Id) return BadRequest();       // Id debe coincidir
            if (!ModelState.IsValid) return View(p);
            p.ModificadoPor = User?.Identity?.Name ?? "sistema";
            var ok = await _repo.UpdateAsync(p);       // Actualiza
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Details), new { id = p.Id });
        }

        // GET: /Propietarios/Delete/5  (borrado lógico)
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Propietarios/Delete/5  (borrado lógico)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = User?.Identity?.Name ?? "sistema";
            var ok = await _repo.LogicalDeleteAsync(id, user);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
