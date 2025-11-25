namespace Evento_Cultural.DTOs
{
    public class EstadisticaArtistaDTO
    {
        public int ArtistaId { get; set; }
        public string Nombre { get; set; } // Coincide con Artista.Nombre
        public int TotalParticipaciones { get; set; }
        public int TotalPremios { get; set; }
    }
}
