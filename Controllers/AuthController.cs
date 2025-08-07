using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaQuala.Data;
using PruebaQuala.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PruebaQuala.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AuthController(AppDbContext context, IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            _context = context;
            _configuration = configuration;
            _env = enviroment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == username))
            {
                ViewBag.Error = "El usuario ya existe";

                return View();
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { UserName = username, PasswordHash = passwordHash }; ;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            var token = GenerateJwtToken(user.UserName);

            SetJwtCookie(token);

            return RedirectToAction("Index", "Producto");

        }

        private void SetJwtCookie(string token)
        {
            Response.Cookies.Append(
                "AuthToken",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = _env.IsProduction(),
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                }
            );
        }

        private string GenerateJwtToken(string userName)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            var key = System.Text.Encoding.UTF8.GetBytes(jwtConfig["Key"]);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                    )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction(nameof(Login));
        }

    }

}
