using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Services.Interfaces;
using BibliotecaApi.Model;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livro;

        public LivroController(ILivroRepository livro)
        {
            _livro = livro;
        }

        [HttpGet("LivrosEmprestados")]
        public async Task<ActionResult<IEnumerable<LivroModel>>> GetUsuariosCadastrados()
        {
            var usuarios = await _livro.ObterLivrosDisponiveis();
            return Ok(usuarios);
        }

        [HttpGet("PesquisarLivro")]
        public async Task<ActionResult<IEnumerable<LivroModel>>> GetLibraryPesquisa([FromQuery] string titulo, [FromQuery] string autor, [FromQuery] string genero)
        {
            try
            {
                var livro = await _livro.PesquisarLivros(titulo, autor, genero);
                return Ok(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Não foi possível encontrar esse livro.");
            } 
        }

        /*
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
        */
        [HttpPost("Cadastramento")]
        public async Task<IActionResult> GetLivroRegistration(LivroModel livro)
        {
            try
            {
                await _livro.EmprestarLivro(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar cadastrar um novo livro.");
            }
            return Ok(livro);
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
                await _livro.DevolverLivro(id);
                return Ok($"Livro de ID {id} devolvido com sucesso.");
            }

            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao devolver o Livro. ID Não existente.");
            }
        }
    }
}
