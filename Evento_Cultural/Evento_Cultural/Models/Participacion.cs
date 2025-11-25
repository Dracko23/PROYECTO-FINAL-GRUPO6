using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    [Table("participaciones", Schema = "eventos")]
    public class Participacion
    {
        [Key]
        [Column("id_participacion")]
        public int Id { get; set; }

        [Column("id_evento")]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        [Column("id_artista")]
        public int ArtistaId { get; set; }
        [ForeignKey("ArtistaId")]
        public Artista Artista { get; set; }

        [Column("hora_presentacion")]
        public DateTime? HoraPresentacion { get; set; }

        [Column("observaciones")]
        public string Observaciones { get; set; }
    }
}