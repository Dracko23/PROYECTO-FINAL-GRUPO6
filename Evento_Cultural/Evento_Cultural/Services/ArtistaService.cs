using Evento_Cultural.Models;
using System.Collections.Generic;
using System.Linq;

namespace Evento_Cultural.Services
{
    public class ArtistaService
    {
        private readonly ApplicationDbContext _context;

        public ArtistaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Artista> ObtenerArtistasActivos()
        {
            return _context.Artistas.Where(a => a.Activo).ToList();
        }

        public int ContarArtistas()
        {
            return _context.Artistas.Count();
        }
    }
}
