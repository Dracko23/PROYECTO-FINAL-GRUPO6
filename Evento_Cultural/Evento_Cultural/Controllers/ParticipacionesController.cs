using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Evento_Cultural.Models;

namespace Evento_Cultural.Controllers
{
    public class ParticipacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Participaciones
        public async Task<IActionResult> Index()
        {
            var participaciones = await _context.Participaciones
                .Include(p => p.Artista)
                .Include(p => p.Evento)
                .ToListAsync();
            return View(participaciones);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.ArtistaId = new SelectList(_context.Artistas.OrderBy(a => a.Nombre), "Id", "Nombre");
            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Titulo), "Id", "Titulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participacion participacion)
        {
            // === ESTO ES LO QUE FALLABA ===
            // Quita la validación automática y hazla manual
            ModelState.Clear(); // ← Línea mágica

            // Validación manual (tú controlas todo)
            if (participacion.ArtistaId <= 0)
            {
                ModelState.AddModelError("ArtistaId", "Debe seleccionar un artista.");
            }

            if (participacion.EventoId <= 0)
            {
                ModelState.AddModelError("EventoId", "Debe seleccionar un evento.");
            }

            // Solo guarda si todo está bien
            if (ModelState.IsValid)
            {
                _context.Add(participacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // ← Aquí sí te lleva al Index
            }

            // Si hay error → recargar los selects con el valor que intentó elegir
            ViewBag.ArtistaId = new SelectList(_context.Artistas.OrderBy(a => a.Nombre), "Id", "Nombre", participacion.ArtistaId);
            ViewBag.EventoId = new SelectList(_context.Eventos.OrderBy(e => e.Titulo), "Id", "Titulo", participacion.EventoId);

            return View(participacion);
        }

        // GET: Participaciones/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var participacion = await _context.Participaciones.FindAsync(id);
            if (participacion == null) return NotFound();

            ViewBag.ArtistaId = new SelectList(_context.Artistas, "Id", "Nombre", participacion.ArtistaId);
            ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Titulo", participacion.EventoId);
            return View(participacion);
        }

        // POST: Participaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Participacion participacion)
        {
            if (id != participacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipacionExists(participacion.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Si hay error de validación
            ViewBag.ArtistaId = new SelectList(_context.Artistas, "Id", "Nombre", participacion.ArtistaId);
            ViewBag.EventoId = new SelectList(_context.Eventos, "Id", "Titulo", participacion.EventoId);
            return View(participacion);
        }

        // GET: Participaciones/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var participacion = await _context.Participaciones
                .Include(p => p.Artista)
                .Include(p => p.Evento)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (participacion == null) return NotFound();
            return View(participacion);
        }

        // POST: Participaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participacion = await _context.Participaciones.FindAsync(id);
            if (participacion != null)
            {
                _context.Participaciones.Remove(participacion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipacionExists(int id)
        {
            return _context.Participaciones.Any(e => e.Id == id);
        }
    }
}