using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using WebApiCatalogo.Catalogo.API.Controllers;
using BibliotecaApi.Library.Application.DTOs;
using AutoMapper;

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
        public async Task<ActionResult<IEnumerable<UserLivroModelDTO>>> GetLivrosEmprestimosTodos()
        {
            var emprestados = await _livro.ObterLivrosEmprestimoDisponiveis();
            return Ok(emprestados);
        }

        //GET: /api/Livro/TodosOsLivros
        [HttpGet]
        [Route("TodosOsLivros")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<LivroModelDTO>>> GetLivrosTodos()
        {
            var emprestados = await _livro.ObterTodosOsLivros();
            return Ok(emprestados);
        }

        //GET: /api/Livro/PesquisarLivroEmprestado
        [HttpGet]
        [Route("PesquisarLivroEmprestado")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<UserLivroModelDTO>>> GetLibraryPesquisa(string? titulo, string? autor, string? genero)
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
        public async Task<ActionResult<LivroModelDTO>> GetLibraryInformation(int id, LivroModelDTO livroDto)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _livro.AtualizarLivros(livroDto);
                return Ok(livroDto);
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
        public async Task<ActionResult<LivroModelDTO>> GetLivroRegistration(LivroModelDTO livroDto)
        {
            try
            {
                await _livro.CadastrarLivro(livroDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar cadastrar um novo livro.");
            }
            return Ok(livroDto);
        }

        //GET: /api/Livro/DevolverLivroEmprestado/id
        [HttpDelete]
        [Route("DevolverLivroEmprestado/{id}")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<LivroModelDTO>> GetLibraryDelete(int id)
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
        public async Task<ActionResult> GetLivroEmprestimo(UserLivroModelDTO emprestimoDto)
        {
            try
            {
                await _livro.EmprestarLivro(emprestimoDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar emprestar um livro.");
            }
            return Ok(emprestimoDto);
        }
    }
}
