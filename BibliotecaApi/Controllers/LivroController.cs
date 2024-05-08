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

        //GET: /api/Livro/LivrosEmprestados
        [HttpGet("LivrosEmprestados")]
        public async Task<ActionResult<IEnumerable<UserLivroModel>>> GetUsuariosCadastrados()
        {
            var emprestados = await _livro.ObterLivrosDisponiveis();
            return Ok(emprestados);
        }

        //GET: /api/Livro/PesquisarLivro
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

        //GET: /api/Livro/EditarUmLivro
        [HttpPut("{id} EditarUmLivro")]
        public async Task<IActionResult> GetLibraryInformation(int id, LivroModel livro)
        {
            if (id == null)
            {
                return NotFound(); 
            }
            try
            {
                await _livro.AtualizarLivros(livro);
                return Ok(livro);
            }

            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar editar um livro.");
            } 
        }

        //GET: /api/Livro/Cadastramento
        [HttpPost("Cadastramento")]
        public async Task<IActionResult> GetLivroRegistration(LivroModel livro)
        {
            try
            {
                await _livro.CadastrarLivro(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar cadastrar um novo livro.");
            }
            return Ok(livro);
        }

        //GET: /api/Livro/ManipulacaoDeUsuario
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

        //GET: /api/Livro/EmprestarLivro
        [HttpPost("EmprestarLivro")]
        public async Task<IActionResult> GetLivroEmprestimo(UserLivroModel emprestimo)
        {
            try
            {
                await _livro.EmprestarLivro(emprestimo);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar emprestar um livro.");
            }
            return Ok(emprestimo);
        }
    }
}
