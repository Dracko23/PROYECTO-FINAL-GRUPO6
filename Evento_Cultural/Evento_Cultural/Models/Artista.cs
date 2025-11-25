using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evento_Cultural.Models
{
    [Table("artistas", Schema = "gestion")]
    public class Artista
    {
        [Key]
        [Column("id_artista")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("genero")]
        public string Genero { get; set; }

        [Column("pais_origen")]
        public string PaisOrigen { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }
    }

}