using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Inmobiliaria.Repositories;
using Inmobiliaria.Models;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioRepository _repo;
        public AccountController(IUsuarioRepository repo) => _repo = repo;

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null || user.PasswordHash != password) // ⚠️ luego reemplazar por hash
            {
                ModelState.AddModelError("", "Email o contraseña inválidos");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }
    }
}
