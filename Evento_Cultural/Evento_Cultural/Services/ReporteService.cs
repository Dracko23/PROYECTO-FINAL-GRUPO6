using Evento_Cultural.Models;
using Evento_Cultural.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Evento_Cultural.Services
{
    public class ReporteService
    {
        private readonly ApplicationDbContext _context;

        public ReporteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ReporteEventoDTO> ObtenerReporteEventos()
        {
            return _context.Eventos
                .Select(e => new ReporteEventoDTO
                {
                    EventoId = e.Id,
                    // CORREGIDO: Usar Titulo en lugar de Nombre
                    Titulo = e.Titulo,
                    CantidadParticipantes = _context.Participaciones
                        .Count(p => p.EventoId == e.Id),
                    TotalVentas = _context.Ventas
                        .Where(v => v.EventoId == e.Id)
                        .Sum(v => (int?)v.Cantidad) ?? 0
                })
                .ToList();
        }
    }
}