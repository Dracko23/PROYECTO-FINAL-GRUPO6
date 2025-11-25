using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    [Table("ventas", Schema = "eventos")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int Id { get; set; }

        [Column("id_evento")]
        public int EventoId { get; set; }

        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        [Column("fecha_venta")]
        public DateTime FechaVenta { get; set; }

        [Column("cliente_nombre")]
        public string ClienteNombre { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("total", TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
    }
}