using Microsoft.AspNetCore.Mvc;
using Evento_Cultural.Services;
using System.Text.Json;

namespace Evento_Cultural.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            // 1. Obtener KPIs
            ViewBag.TotalIngresos = _service.TotalIngresos();
            ViewBag.TotalVentas = _service.TotalEntradasVendidas();
            ViewBag.TotalEventos = _service.TotalEventos();
            ViewBag.TotalArtistas = _service.TotalArtistas();
            ViewBag.TotalGanadores = _service.TotalGanadores();

            // 2. Datos para Gráficos (Serializados a JSON para usarlos en JavaScript)
            var ventasData = _service.VentasPorEvento();
            var participacionData = _service.ParticipacionArtistas();

            ViewBag.VentasJson = JsonSerializer.Serialize(ventasData);
            ViewBag.ParticipacionJson = JsonSerializer.Serialize(participacionData);

            return View();
        }
    }
}