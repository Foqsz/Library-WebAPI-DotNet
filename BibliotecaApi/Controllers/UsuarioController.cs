using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Repositorios.Interfaces;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuario;

        public UsuarioController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        [HttpGet("UsuariosCadastrados")]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuariosCadastrados()
        {
            var usuarios = await _usuario.ObterUsuariosCadastrados();
            return Ok(usuarios);
        }

        [HttpGet("{id} PesquisaPorId")]
        public async Task<ActionResult<UsuarioModel>> GetLibraryPesquisa(int id)
        {
            var usuario = await _usuario.ObterUsuarioPorId(id);
            if (id == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPut("{id} EditarSuasInformacoes")]
        public async Task<IActionResult> GetLibraryInformation(int id, UsuarioModel usuario)
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
        public async Task<IActionResult> GetLibraryRegistration(UsuarioModel novoUsuario)
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
