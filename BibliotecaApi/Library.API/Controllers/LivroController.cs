using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using WebApiCatalogo.Catalogo.API.Controllers;

namespace BibliotecaApi.Library.API.Controllers
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

        //GET: /api/Livro/LivrosEmprestados
        [HttpGet]
        [Route("LivrosEmprestados")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<UserLivroModel>>> GetLivrosEmprestimosTodos()
        {
            var emprestados = await _livro.ObterLivrosEmprestimoDisponiveis();
            return Ok(emprestados);
        }

        //GET: /api/Livro/TodosOsLivros
        [HttpGet]
        [Route("TodosOsLivros")]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<LivroModel>>> GetLivrosTodos()
        {
            var emprestados = await _livro.ObterTodosOsLivros();
            return Ok(emprestados);
        }

        //GET: /api/Livro/PesquisarLivroEmprestado
        [HttpGet]
        [Route("PesquisarLivroEmprestado")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<UserLivroModel>>> GetLibraryPesquisa(string? titulo, string? autor, string? genero)
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

        //GET: /api/Livro/EditarUmLivro/id
        [HttpPut]
        [Route("EditarUmLivro/{id}")]
        //[Authorize(Policy = "AdminOnly")]
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
        [HttpPost]
        [Route("Cadastramento")]
        //[Authorize(Policy = "UserOnly")]
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

        //GET: /api/Livro/DevolverLivroEmprestado/id
        [HttpDelete]
        [Route("DevolverLivroEmprestado/{id}")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetLibraryDelete(int id)
        {
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
        [HttpPost]
        [Route("EmprestarLivro")]
        //[Authorize(Policy = "UserOnly")]
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
