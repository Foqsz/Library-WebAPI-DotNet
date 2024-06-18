using BibliotecaApi.Library.Application.DTOs;
using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using BibliotecaApi.Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BibliotecaApi.Library.Infrastructure.Repository
{
    public class LivroRepository : ILivroRepository
    {

        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public LivroRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AtualizarLivros(LivroModelDTO livroDto)
        {
            var livro = _mapper.Map<LivroModel>(livroDto);
            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DevolverLivro(int livroId)
        {
            var livro = await _context.userLivroEmprestimo.FirstOrDefaultAsync(l => l.Id == livroId);

            if (livro != null)
            {
                _context.userLivroEmprestimo.Remove(livro);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Livro não encontrado.");
            }
        }


        public async Task CadastrarLivro(LivroModelDTO livroDto)
        {
            bool livroExistente = await _context.livroModels.AnyAsync(l => l.Titulo == livroDto.Titulo);

            if (!livroExistente)
            {
                var livroAdd = _mapper.Map<LivroModel>(livroDto);
                _context.livroModels.Add(livroAdd);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Livro já cadastrado.");
            }
        }


        public async Task<IEnumerable<UserLivroModelDTO>> ObterLivrosEmprestimoDisponiveis()
        {
            var userLivroModels = await _context.userLivroEmprestimo.ToListAsync();
            var dtos = _mapper.Map<List<UserLivroModelDTO>>(userLivroModels);
            return dtos;
        }

        public async Task<IEnumerable<UserLivroModelDTO>> PesquisarLivros(string? titulo, string? autor, string? genero)
        {
            var query = _context.userLivroEmprestimo.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
            {
                query = query.Where(l => l.NomeLivro.Contains(titulo));
            }

            if (!string.IsNullOrEmpty(autor))
            {
                query = query.Where(l => l.Autor.Contains(autor));
            }

            if (!string.IsNullOrEmpty(genero))
            {
                query = query.Where(l => l.Genero.Contains(genero));
            }

            var userLivroModels = await query.ToListAsync();
            var queryDto = _mapper.Map<List<UserLivroModelDTO>>(userLivroModels);
            return queryDto;
        }

        public async Task EmprestarLivro(UserLivroModelDTO emprestimo)
        {
            bool livroExistente = await _context.livroModels.AnyAsync(l => l.Titulo == emprestimo.NomeLivro);

            if (!livroExistente)
            {
                var userLivroModel = _mapper.Map<UserLivroModel>(emprestimo);
                _context.userLivroEmprestimo.Add(userLivroModel);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Livro já emprestado.");
            }
        }

        public async Task<IEnumerable<LivroModelDTO>> ObterTodosOsLivros()
        {
            var livroModels = await _context.livroModels.ToListAsync();
            var livroDtos = _mapper.Map<List<LivroModelDTO>>(livroModels);
            return livroDtos;
        }
    }
}
