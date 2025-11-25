using Evento_Cultural.Models;
using System.Collections.Generic;
using System.Linq;

namespace Evento_Cultural.Services
{
    public class EventoService
    {
        private readonly ApplicationDbContext _context;

        public EventoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Cambiar 'Activo' por 'Estado'
        // Total de eventos activos
        public int TotalEventos()
        {
            return _context.Eventos.Count(e => e.Estado == "PROGRAMADO" || e.Estado == "EN CURSO");
        }


        // Contar todos los eventos
        public int ContarEventos()
        {
            return _context.Eventos.Count();
        }
    }
}
