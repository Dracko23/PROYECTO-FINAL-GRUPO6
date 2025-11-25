using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Evento_Cultural.Models;

namespace Evento_Cultural.Controllers
{
    public class EntradasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntradasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var entradas = _context.Entradas
                .Include(e => e.Evento)
                .OrderBy(e => e.Evento.Titulo)
                .ThenBy(e => e.TipoEntrada);
            return View(await entradas.ToListAsync());
        }

        // CREATE GET
        public IActionResult Create()
        {
            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Titulo), "Id", "Titulo");
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Entrada entrada)
        {
            ModelState.Clear();

            if (entrada.EventoId <= 0)
                ModelState.AddModelError("EventoId", "Debe seleccionar un evento.");
            if (string.IsNullOrWhiteSpace(entrada.TipoEntrada))
                ModelState.AddModelError("TipoEntrada", "El tipo de entrada es obligatorio.");
            if (entrada.Precio <= 0)
                ModelState.AddModelError("Precio", "El precio debe ser mayor a 0.");

            if (ModelState.IsValid)
            {
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Titulo), "Id", "Titulo", entrada.EventoId);
            return View(entrada);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada == null) return NotFound();

            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Titulo), "Id", "Titulo", entrada.EventoId);
            return View(entrada);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Entrada entrada)
        {
            if (id != entrada.Id) return NotFound();

            ModelState.Clear();

            if (entrada.EventoId <= 0)
                ModelState.AddModelError("EventoId", "Debe seleccionar un evento.");
            if (string.IsNullOrWhiteSpace(entrada.TipoEntrada))
                ModelState.AddModelError("TipoEntrada", "El tipo de entrada es obligatorio.");
            if (entrada.Precio <= 0)
                ModelState.AddModelError("Precio", "El precio debe ser mayor a 0.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrada);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    ModelState.AddModelError("", "Error al actualizar.");
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Titulo), "Id", "Titulo", entrada.EventoId);
            return View(entrada);
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var entrada = await _context.Entradas.Include(e => e.Evento).FirstOrDefaultAsync(e => e.Id == id);
            if (entrada == null) return NotFound();
            return View(entrada);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var entrada = await _context.Entradas.Include(e => e.Evento).FirstOrDefaultAsync(e => e.Id == id);
            if (entrada == null) return NotFound();
            return View(entrada);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}