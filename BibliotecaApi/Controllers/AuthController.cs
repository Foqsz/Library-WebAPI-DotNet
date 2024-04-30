using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using BibliotecaApi.Services.Interfaces;
using System.Threading.Tasks;
using BibliotecaApi.Services;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            var user = new UsuarioModel(
                1,
                "Victor Vinicius",
                "victor@gmail.com",
                "q1w2e3r4t5");

            var token = _authService.GenerateJwtToken(user);

            return Ok(new { token });
        }
    }
}
