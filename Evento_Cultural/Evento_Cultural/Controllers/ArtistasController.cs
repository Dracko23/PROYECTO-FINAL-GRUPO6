using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evento_Cultural.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Evento_Cultural.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            var artistas = await _context.Artistas.ToListAsync();
            return View(artistas);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artistas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artista artista)
        {
            if (ModelState.IsValid)
            {
                _context.Artistas.Add(artista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null) return NotFound();

            return View(artista);
        }

        // POST: Artistas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Artista artista)
        {
            if (id != artista.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistaExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null) return NotFound();

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            if (artista != null)
            {
                _context.Artistas.Remove(artista);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null) return NotFound();

            return View(artista);
        }

        private bool ArtistaExists(int id)
        {
            return _context.Artistas.Any(e => e.Id == id);
        }
    }
}
