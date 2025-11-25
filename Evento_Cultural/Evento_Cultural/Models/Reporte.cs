using System;

namespace Evento_Cultural.Models
{
    public class Reporte
    {
        public int Id { get; set; }
        public string TipoReporte { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
        public string GeneradoPor { get; set; }
    }
}
