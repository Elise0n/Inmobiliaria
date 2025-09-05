using System.Threading.Tasks;                         // Para Task/async
using Microsoft.AspNetCore.Mvc;                      // Base MVC
using Inmobiliaria.Repositories;                  // Repositorios
using Inmobiliaria.Models;                        // Modelos

namespace Inmobiliaria.Controllers
{
    /// <summary>
    /// Controlador MVC para ABM de Inquilinos.
    /// </summary>
    public class InquilinosController : Controller
    {
        private readonly IInquilinoRepository _repo; // Inyección del repo

        public InquilinosController(IInquilinoRepository repo)
        {
            _repo = repo;                              // Guardamos el repo
        }

        // GET: /Inquilinos
        public async Task<IActionResult> Index()
        {
            var lista = await _repo.GetAllAsync();     // Obtenemos todos
            return View(lista);                        // Pasamos la lista a la vista
        }

        // GET: /Inquilinos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _repo.GetByIdAsync(id);   // Buscamos por Id
            if (item == null) return NotFound();       // 404 si no existe
            return View(item);                         // Mostramos la vista
        }

        // GET: /Inquilinos/Create
        public IActionResult Create() => View();       // Devuelve formulario vacío

        // POST: /Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inquilino i)
        {
            if (!ModelState.IsValid) return View(i);   // Valida modelo
            i.CreadoPor = User?.Identity?.Name ?? "sistema"; // Auditoría simple
            var id = await _repo.CreateAsync(i);       // Inserta en BD
            return RedirectToAction(nameof(Details), new { id }); // Redirige a Details
        }

        // GET: /Inquilinos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inquilino i)
        {
            if (id != i.Id) return BadRequest();       // Id debe coincidir
            if (!ModelState.IsValid) return View(i);
            i.ModificadoPor = User?.Identity?.Name ?? "sistema";
            var ok = await _repo.UpdateAsync(i);       // Actualiza
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Details), new { id = i.Id });
        }

        // GET: /Inquilinos/Delete/5  (borrado lógico)
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Inquilinos/Delete/5  (borrado lógico)
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
