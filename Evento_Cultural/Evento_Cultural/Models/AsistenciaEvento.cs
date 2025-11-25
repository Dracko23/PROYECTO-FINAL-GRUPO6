using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    public class AsistenciaEvento
    {
        public int Id { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }

        public int TotalAsistentes { get; set; }

        public decimal PorcentajeOcupacion { get; set; }

        public Evento Evento { get; set; }
    }
}
