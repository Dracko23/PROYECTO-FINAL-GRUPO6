using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    [Table("eventos", Schema = "eventos")]
    public class Evento
    {
        [Key]
        [Column("id_evento")]
        public int Id { get; set; }

        [Required]
        [Column("titulo")]
        public string Titulo { get; set; } // En las vistas usaremos "Titulo"

        [Column("tipo_evento")]
        public string TipoEvento { get; set; }

        [Column("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin")]
        public DateTime? FechaFin { get; set; }

        [Column("lugar")]
        public string Lugar { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("capacidad")]
        public int? Capacidad { get; set; }

        [Column("estado")]
        public string Estado { get; set; } // 'PROGRAMADO', 'EN CURSO', etc.
    }
}