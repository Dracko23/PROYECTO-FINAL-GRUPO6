using Evento_Cultural.Models;
using Evento_Cultural.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// SOLUCIÓN AL ERROR DE FECHAS DE POSTGRESQL
// Esto permite guardar fechas sin zona horaria (Kind=Unspecified)
// ==================================================================
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// 1. Configuración de la Base de Datos
// Asegúrate de que la cadena de conexión en appsettings.json sea correcta
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Inyección de Dependencias (Servicios)
builder.Services.AddScoped<DashboardService>();
// Aquí puedes agregar otros servicios si los creas (ej: EventoService, ArtistaService)

// 3. Configuración de MVC (Vistas y Controladores)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración del pipeline de peticiones HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();