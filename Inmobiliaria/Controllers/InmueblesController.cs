using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Inmobiliaria.Repositories;

namespace Inmobiliaria.Controllers
{
    /// <summary>
    /// Controlador MVC para ABM de Inmuebles.
    /// </summary>
    public class InmueblesController : Controller
    {
        private readonly IInmuebleRepository _repo;

        public InmueblesController(IInmuebleRepository repo)
        {
            _repo = repo;
        }

        // GET: /Inmuebles
        public async Task<IActionResult> Index()
        {
            var lista = await _repo.GetAllAsync();
            return View(lista);
        }

        // GET: /Inmuebles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /Inmuebles/Create
        public IActionResult Create() => View();

        // POST: /Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inmueble i)
        {
            if (!ModelState.IsValid) return View(i);
            i.CreadoPor = User?.Identity?.Name ?? "sistema";
            var id = await _repo.CreateAsync(i);
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Inmuebles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inmueble i)
        {
            if (id != i.Id) return BadRequest();
            if (!ModelState.IsValid) return View(i);
            i.ModificadoPor = User?.Identity?.Name ?? "sistema";
            var ok = await _repo.UpdateAsync(i);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Details), new { id = i.Id });
        }

        // GET: /Inmuebles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Inmuebles/Delete/5
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
