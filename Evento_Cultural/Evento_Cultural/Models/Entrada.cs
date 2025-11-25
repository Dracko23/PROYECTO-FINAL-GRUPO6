using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    [Table("entradas", Schema = "eventos")]
    public class Entrada
    {
        [Key]
        [Column("id_entrada")]
        public int Id { get; set; }

        [Column("id_evento")]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        [Column("tipo_entrada")]
        public string TipoEntrada { get; set; }

        [Column("precio", TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("stock_minimo")]
        public int StockMinimo { get; set; }
    }
}