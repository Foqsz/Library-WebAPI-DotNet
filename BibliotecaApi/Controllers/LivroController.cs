using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore; 
using BibliotecaApi.Services.Interfaces;
using BibliotecaApi.Model;

namespace BibliotecaApi.Controllers
{
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

        [HttpGet("{name} PesquisarLivro")]
        public async Task<ActionResult<UsuarioModel>> GetLibraryPesquisa(string name, string titulo, string autor, string genero)
        {
            var livro = await _livro.PesquisarLivros(titulo, autor, genero);
            if (name == null)
            {
                return NotFound();
            }
            return Ok(livro);
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
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpDelete("{name} ManipulacaoDeUsuario")]
        public async Task<IActionResult> GetLibraryDelete(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            try
            {
                await _livro.DevolverLivro(name);
            }

            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Ok(name);
        }
    }
}
