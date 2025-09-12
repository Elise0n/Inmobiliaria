// using: directivas para importar espacios de nombres necesarios

using Microsoft.AspNetCore.Builder; // tipos para construir la app web
using Microsoft.Extensions.DependencyInjection; // para registrar servicios en el contenedor de DI
using Microsoft.Extensions.Hosting;                // Para detectar entorno (Development/Production)
using Inmobiliaria.Data; // Namespace donde est� DbConnectionFactory
using Inmobiliaria.Repositories; // Namespace donde est�n los repositorios


var builder = WebApplication.CreateBuilder(args);  // Crea el "host" y carga configuraci�n (appsettings, env, etc.)
builder.Services.AddTransient<IPropietarioRepository, PropietarioRepository>(); // Inyecta PropietarioRepository
builder.Services.AddTransient<IInquilinoRepository, InquilinoRepository>(); // Inyecta InquilinoRepository
builder.Services.AddTransient<IInmuebleRepository, InmuebleRepository>();   // Inyecta InmuebleRepository
builder.Services.AddTransient<IContratoRepository, ContratoRepository>();   // Inyecta ContratoRepository
builder.Services.AddTransient<IPagoRepository, PagoRepository>();         // Inyecta PagoRepository/
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login"; // Redirige si no est� logueado
    });

builder.Services.AddAuthorization();


// registra la factory
builder.Services.AddTransient<DbConnectionFactory>();

// Registramos servicios que usar� la app (MVC con Vistas)
builder.Services.AddControllersWithViews(); // Habilita el patr�n MVC y las vistas Razor

var app = builder.Build();                  // Construye la aplicaci�n con los servicios registrados

// Configuraci�n del pipeline HTTP (orden IMPORTA)
if (!app.Environment.IsDevelopment())       // Si NO estamos en desarrollo...
{
    app.UseExceptionHandler("/Home/Error"); // P�gina de error amigable (en producci�n)
    app.UseHsts();                          // HSTS: seguridad para tr�fico HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");// Si no se especifica, va a Home/Index

app.Run();   // Arranca la aplicaci�n web
