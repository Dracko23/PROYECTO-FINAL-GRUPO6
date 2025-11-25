using Evento_Cultural.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Evento_Cultural.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Métodos KPI
        public int TotalArtistas() => _context.Artistas.Count(a => a.Activo);

        public int TotalEventos() => _context.Eventos.Count(e => e.Estado == "PROGRAMADO" || e.Estado == "EN CURSO");

        public int TotalEntradasVendidas() => _context.Ventas.Sum(v => (int?)v.Cantidad) ?? 0;

        public decimal TotalIngresos() => _context.Ventas.Sum(v => (decimal?)v.Total) ?? 0;

        public int TotalGanadores() => _context.Ganadores.Count();

        // Datos para Gráficos (Ventas por Evento)
        public List<dynamic> VentasPorEvento()
        {
            // Usamos una proyección anónima para Chart.js
            var datos = _context.Ventas
                .Include(v => v.Evento)
                .GroupBy(v => v.Evento.Titulo)
                .Select(g => new {
                    name = g.Key,
                    value = g.Sum(x => x.Cantidad)
                })
                .Take(5)
                .ToList<dynamic>();

            return datos;
        }

        // Participación de artistas
        public List<dynamic> ParticipacionArtistas()
        {
            var datos = _context.Participaciones
               .Include(p => p.Artista)
               .GroupBy(p => p.Artista.Nombre)
               .Select(g => new {
                   name = g.Key,
                   value = g.Count()
               })
               .Take(5)
               .ToList<dynamic>();
            return datos;
        }
    }
}