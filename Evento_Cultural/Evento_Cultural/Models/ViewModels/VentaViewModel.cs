// Models/ViewModels/VentaViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace Evento_Cultural.Models.ViewModels
{
    public class VentaViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar un evento")]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de entrada")]
        public int EntradaId { get; set; }

        public string ClienteNombre { get; set; } = "Consumidor Final";

        [Range(1, 1000, ErrorMessage = "Cantidad mínima es 1")]
        public int Cantidad { get; set; } = 1;

        // Para la vista
        public List<Evento> Eventos { get; set; } = new();
        public decimal PrecioUnitario { get; set; }
        public decimal Total => PrecioUnitario * Cantidad;
    }
}