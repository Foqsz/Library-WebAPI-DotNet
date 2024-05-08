using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using BibliotecaApi.Services.Interfaces;
using System.Threading.Tasks;
using BibliotecaApi.Services;
using BibliotecaApi.Repository;

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
         
        //POST: api/Auth/GerarToken
        [HttpPost("GerarToken")]
        public IActionResult GerarToken()
        {
            var user = new UsuarioModel(
                1,
                "Victor Vinicius",
                "victor@gmail.com",
                "q1w2e3r4t5");

            var token = _authService.GenerateJwtToken(user);

            return Ok(new { token });
        }
        /*
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UsuarioModel login)
        { 
            if (string.IsNullOrEmpty(login.Name) || string.IsNullOrEmpty(login.Senha))
            {
                return BadRequest("Por favor, forneça o nome de usuário e a senha.");
            }
             
            var usuario = await _authService.ObterUsuarioPorNomeESenha(login.Name, login.Senha);
             
            if (usuario == null)
            { 
                return BadRequest("Nome de usuário ou senha incorretos.");
            } 
        }*/

    }
}
