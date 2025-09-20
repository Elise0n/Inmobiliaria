using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Inmobiliaria.Repositories;

namespace Inmobiliaria.Controllers
{
    public class ContratosController : Controller
    {
        private readonly IContratoRepository _repo;
        public ContratosController(IContratoRepository repo) => _repo = repo;

        public async Task<IActionResult> Index()
        {
            var lista = await _repo.GetAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contrato c)
        {
            if (!ModelState.IsValid) return View(c);
            c.CreadoPor = User?.Identity?.Name ?? "sistema";
            var id = await _repo.CreateAsync(c);
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contrato c)
        {
            if (id != c.Id) return BadRequest();
            if (!ModelState.IsValid) return View(c);
            //validacion
            if (await _repo.ExisteSuperposicionAsync(c.InmuebleId, c.FechaInicio, c.FechaFin))
            {
                ModelState.AddModelError("", "El inmueble ya tiene un contrato en esas fechas.");
                return View(c);// vuelve a la vista con error
            }
            c.ModificadoPor = User?.Identity?.Name ?? "sistema";
            var ok = await _repo.UpdateAsync(c);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Details), new { id = c.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _repo.LogicalDeleteAsync(id, User?.Identity?.Name ?? "sistema");
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
