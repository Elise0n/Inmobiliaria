// using: directivas para importar espacios de nombres necesarios

using Microsoft.AspNetCore.Builder; // tipos para construir la app web
using Microsoft.Extensions.DependencyInjection; // para registrar servicios en el contenedor de DI
using Microsoft.Extensions.Hosting;                // Para detectar entorno (Development/Production)
using Inmobiliaria.Data; // Namespace donde está DbConnectionFactory
using Inmobiliaria.Repositories; // Namespace donde están los repositorios


var builder = WebApplication.CreateBuilder(args);  // Crea el "host" y carga configuración (appsettings, env, etc.)
builder.Services.AddTransient<IPropietarioRepository, PropietarioRepository>(); // Inyecta PropietarioRepository
builder.Services.AddTransient<IInquilinoRepository, InquilinoRepository>(); // Inyecta InquilinoRepository
builder.Services.AddTransient<IInmuebleRepository, InmuebleRepository>();   // Inyecta InmuebleRepository
builder.Services.AddTransient<IContratoRepository, ContratoRepository>();   // Inyecta ContratoRepository
builder.Services.AddTransient<IPagoRepository, PagoRepository>();         // Inyecta PagoRepository/
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login"; // Redirige si no está logueado
    });

builder.Services.AddAuthorization();


// registra la factory
builder.Services.AddTransient<DbConnectionFactory>();

// Registramos servicios que usará la app (MVC con Vistas)
builder.Services.AddControllersWithViews(); // Habilita el patrón MVC y las vistas Razor

var app = builder.Build();                  // Construye la aplicación con los servicios registrados

// Configuración del pipeline HTTP (orden IMPORTA)
if (!app.Environment.IsDevelopment())       // Si NO estamos en desarrollo...
{
    app.UseExceptionHandler("/Home/Error"); // Página de error amigable (en producción)
    app.UseHsts();                          // HSTS: seguridad para tráfico HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");// Si no se especifica, va a Home/Index

app.Run();   // Arranca la aplicación web
