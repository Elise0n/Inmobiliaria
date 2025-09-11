using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Inmobiliaria.Repositories;

namespace Inmobiliaria.Controllers
{
    /// Controlador para gestionar pagos de contratos.
    public class PagosController : Controller
    {
        private readonly IPagoRepository _repo;
        public PagosController(IPagoRepository repo) => _repo = repo;

        // Lista pagos por contrato
        public async Task<IActionResult> Index(int contratoId)
        {
            ViewBag.ContratoId = contratoId;
            var lista = await _repo.GetAllByContratoAsync(contratoId);
            return View(lista);
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        public IActionResult Create(int contratoId)
        {
            var p = new Pago { ContratoId = contratoId };
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pago p)
        {
            if (!ModelState.IsValid) return View(p);
            p.CreadoPor = User?.Identity?.Name ?? "sistema";
            var id = await _repo.CreateAsync(p);
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pago p)
        {
            if (id != p.Id) return BadRequest();
            p.ModificadoPor = User?.Identity?.Name ?? "sistema";
            var ok = await _repo.UpdateAsync(p);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Details), new { id = p.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _repo.AnularAsync(id, User?.Identity?.Name ?? "sistema");
            if (!ok) return NotFound();
            return RedirectToAction("Index"); // Podría pasarse contratoId en ViewBag
        }
    }
}
