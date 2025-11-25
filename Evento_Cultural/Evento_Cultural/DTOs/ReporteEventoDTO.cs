namespace Evento_Cultural.DTOs
{
    public class ReporteEventoDTO
    {
        public int EventoId { get; set; }
        public string Titulo { get; set; } // <- agregar
        public int CantidadParticipantes { get; set; }
        public int TotalVentas { get; set; }
    }
}
