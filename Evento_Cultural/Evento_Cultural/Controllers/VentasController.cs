using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Evento_Cultural.Models;
using Evento_Cultural.Models.ViewModels;

namespace Evento_Cultural.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ventas = _context.Ventas
                .Include(v => v.Evento)
                .OrderByDescending(v => v.FechaVenta);
            return View(await ventas.ToListAsync());
        }

        // CREATE GET
        public IActionResult Create()
        {
            var model = new VentaViewModel
            {
                Eventos = _context.Eventos.OrderBy(e => e.Titulo).ToList()
            };
            return View(model);
        }

        // AJAX: Obtener entradas por evento
        [HttpGet]
        public JsonResult GetEntradas(int eventoId)
        {
            var entradas = _context.Entradas
                .Where(e => e.EventoId == eventoId)
                .Select(e => new
                {
                    id = e.Id,
                    tipo = e.TipoEntrada,
                    precio = e.Precio
                })
                .ToList();

            return Json(entradas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,EntradaId,ClienteNombre,Cantidad")] VentaViewModel model)
        {
            // Validación manual por si acaso
            if (model.EventoId <= 0)
                ModelState.AddModelError("EventoId", "Selecciona un evento.");

            if (model.EntradaId <= 0)
                ModelState.AddModelError("EntradaId", "Selecciona un tipo de entrada.");

            if (model.Cantidad <= 0)
                ModelState.AddModelError("Cantidad", "La cantidad debe ser mayor a 0.");

            if (!ModelState.IsValid)
            {
                model.Eventos = await _context.Eventos.OrderBy(e => e.Titulo).ToListAsync();
                return View(model);
            }

            // OBTENER LA ENTRADA PARA SACAR EL PRECIO
            var entrada = await _context.Entradas
                .FirstOrDefaultAsync(e => e.Id == model.EntradaId && e.EventoId == model.EventoId);

            if (entrada == null)
            {
                ModelState.AddModelError("", "La entrada seleccionada no es válida para este evento.");
                model.Eventos = await _context.Eventos.OrderBy(e => e.Titulo).ToListAsync();
                return View(model);
            }

            // CREAR LA VENTA
            var venta = new Venta
            {
                EventoId = model.EventoId,
                ClienteNombre = string.IsNullOrWhiteSpace(model.ClienteNombre) ? "Consumidor Final" : model.ClienteNombre.Trim(),
                Cantidad = model.Cantidad,
                Total = entrada.Precio * model.Cantidad,
                FechaVenta = DateTime.Now
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Resto del CRUD (Details, Delete, etc.) → igual que antes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var venta = await _context.Ventas.Include(v => v.Evento).FirstOrDefaultAsync(v => v.Id == id);
            if (venta == null) return NotFound();
            return View(venta);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var venta = await _context.Ventas.Include(v => v.Evento).FirstOrDefaultAsync(v => v.Id == id);
            if (venta == null) return NotFound();
            return View(venta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}