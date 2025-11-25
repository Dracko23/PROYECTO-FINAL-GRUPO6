using Evento_Cultural.Models;
using System.Linq;

namespace Evento_Cultural.Services
{
    public class VentaService
    {
        private readonly ApplicationDbContext _context;

        public VentaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal CalcularIngresosTotales()
        {
            return _context.Ventas.Sum(v => v.Total);
        }

        public int ContarVentas()
        {
            return _context.Ventas.Sum(v => v.Cantidad);
        }
    }
}
