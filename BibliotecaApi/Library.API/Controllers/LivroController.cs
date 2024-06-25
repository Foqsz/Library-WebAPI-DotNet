using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using WebApiCatalogo.Catalogo.API.Controllers;
using BibliotecaApi.Library.Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace BibliotecaApi.Library.API.Controllers
{  
    [Route("api/[controller]")] 
    [ApiController] 
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livro;
        private readonly IMapper _mapper;

        public LivroController(ILivroRepository livro, IMapper mapper)
        {
            _livro = livro;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os livros emprestados
        /// </summary>
        /// <returns>Retorna os livros emprestados</returns>
        //GET: /api/Livro/LivrosEmprestados  
        [HttpGet]
        [Route("LivrosEmprestados")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<UserLivroModelDTO>>> GetLivrosEmprestimosTodos()
        {
            var emprestados = await _livro.ObterLivrosEmprestimoDisponiveis();

            var emprestadosDto = _mapper.Map<IEnumerable<UserLivroModel>>(emprestados);

            return Ok(emprestadosDto);
        }

        /// <summary>
        /// Obtém todos os livros
        /// </summary>
        /// <returns>Retorna todos os livros</returns>
        //GET: /api/Livro/TodosOsLivros
        [HttpGet]
        [Route("TodosOsLivros")]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<LivroModelDTO>>> GetLivrosTodos()
        {
            var todosLivros = await _livro.ObterTodosOsLivros();

            var todosLivrosDto = _mapper.Map<IEnumerable<LivroModel>>(todosLivros);

            return Ok(todosLivrosDto);
        }

        /// <summary>
        /// Pesquisa um livro emprestado
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="autor"></param>
        /// <param name="genero"></param>
        /// <returns>Retorna um livro emprestado</returns>
        //GET: /api/Livro/PesquisarLivroEmprestado
        [HttpGet]
        [Route("PesquisarLivroEmprestado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Edita um livro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livroDto"></param>
        /// <returns>Retorna um livro editado</returns>
        //GET: /api/Livro/EditarUmLivro/id
        [HttpPut]
        [Route("EditarUmLivro/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Cadastra um novo livro
        /// </summary>
        /// <param name="livroDto"></param>
        /// <returns>Retorna um livro cadastrado</returns>
        //GET: /api/Livro/Cadastramento
        [HttpPost]
        [Route("Cadastramento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Devolve um livro emprestado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devolve um livro</returns>
        //GET: /api/Livro/DevolverLivroEmprestado/id
        [HttpDelete]
        [Route("DevolverLivroEmprestado/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Empresta um livro
        /// </summary>
        /// <param name="emprestimoDto"></param>
        /// <returns>Retorna um livro emprestado</returns>
        //GET: /api/Livro/EmprestarLivro
        [HttpPost]
        [Route("EmprestarLivro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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
