using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Evento_Cultural.Models;

namespace Evento_Cultural.Controllers
{
    public class GanadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GanadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ganadores = _context.Ganadores.Include(g => g.Evento).Include(g => g.Artista);
            return View(await ganadores.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Titulo");
            ViewData["ArtistaId"] = new SelectList(_context.Artistas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ganador ganador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ganador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // CORRECCIÓN
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Titulo", ganador.EventoId);
            ViewData["ArtistaId"] = new SelectList(_context.Artistas, "Id", "Nombre", ganador.ArtistaId);
            return View(ganador);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ganador = await _context.Ganadores
                .Include(g => g.Evento)
                .Include(g => g.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ganador == null) return NotFound();
            return View(ganador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ganador = await _context.Ganadores.FindAsync(id);
            if (ganador != null)
            {
                _context.Ganadores.Remove(ganador);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ganador = await _context.Ganadores
                .Include(g => g.Evento)
                .Include(g => g.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ganador == null) return NotFound();
            return View(ganador);
        }
    }
}