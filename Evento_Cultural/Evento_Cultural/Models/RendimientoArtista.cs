using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    public class RendimientoArtista
    {
        public int Id { get; set; }

        [ForeignKey("Artista")]
        public int ArtistaId { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }

        public int Calificacion { get; set; }

        public string? Observaciones { get; set; }

        public Artista Artista { get; set; }
        public Evento Evento { get; set; }
    }
}
