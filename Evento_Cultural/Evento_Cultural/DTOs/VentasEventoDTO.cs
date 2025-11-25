namespace Evento_Cultural.DTOs
{
    public class VentasEventoDTO
    {
        public int EventoId { get; set; }
        public string NombreEvento { get; set; }  // Coincide con Evento.Nombre
        public int EntradasVendidas { get; set; }
        public decimal TotalIngresos { get; set; }
    }
}
