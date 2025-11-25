using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    [Table("ganadores", Schema = "eventos")]
    public class Ganador
    {
        [Key]
        [Column("id_ganador")]
        public int Id { get; set; }

        [Column("id_evento")]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        [Column("id_artista")]
        public int ArtistaId { get; set; }
        [ForeignKey("ArtistaId")]
        public Artista Artista { get; set; }

        [Column("puesto")]
        public string Puesto { get; set; }

        [Column("premio")]
        public string Premio { get; set; }

        [Column("observaciones")]
        public string Observaciones { get; set; }
    }
}