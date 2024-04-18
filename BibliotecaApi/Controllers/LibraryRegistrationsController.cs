using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using BibliotecaApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryRegistrationsController : ControllerBase
    {
        private readonly IUsuarioServiceDTO _usuario;

        public LibraryRegistrationsController(IUsuarioServiceDTO usuario)
        {
            _usuario = usuario;
        }

        [HttpGet("UsuariosCadastrados")]
        public async Task<ActionResult<IEnumerable<ServiceUsuarioDTO>>> GetUsuariosCadastrados()
        {
            var usuarios = await _usuario.ObterUsuariosCadastrados();
            return Ok(usuarios);
        }

        [HttpGet("{id} PesquisaPorId")]
        public async Task<ActionResult<ServiceUsuarioDTO>> GetLibraryPesquisa(int id)
        {
            var usuario = await _usuario.ObterUsuarioPorId(id);
            if (id == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPut("{id} EditarSuasInformacoes")]
        public async Task<IActionResult> GetLibraryInformation(int id, ServiceUsuarioDTO usuario)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _usuario.AtualizarUsuario(usuario);
            }

            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost("Cadastramento")]
        public async Task<IActionResult> GetLibraryRegistration(ServiceUsuarioDTO novoUsuario)
        { 
            try
            {
                await _usuario.InserirUsuario(novoUsuario);
            }

            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Ok(novoUsuario);
        }

        [HttpDelete("{id} ManipulacaoDeUsuario")]
        public async Task<IActionResult> GetLibraryDelete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _usuario.ExcluirUsuario(id);
            }

            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }  
            return Ok(id);
        } 
    }
}
