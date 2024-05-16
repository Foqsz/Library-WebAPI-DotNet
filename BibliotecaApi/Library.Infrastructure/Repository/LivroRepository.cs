using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using BibliotecaApi.Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Library.Infrastructure.Repository
{
    public class LivroRepository : ILivroRepository
    {

        private readonly LibraryContext _context;

        public LivroRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task AtualizarLivros(LivroModel livro)
        {
            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DevolverLivro(int id)
        {
            var livro = await _context.userLivroEmprestimo.FindAsync(id);
            if (livro != null)
            {
                _context.userLivroEmprestimo.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CadastrarLivro(LivroModel livro)
        {
            _context.livroModels.Add(livro);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserLivroModel>> ObterLivrosEmprestimoDisponiveis()
        {
            return await _context.userLivroEmprestimo.ToListAsync();
        }

        public async Task<IEnumerable<UserLivroModel>> PesquisarLivros(string? titulo, string? autor, string? genero)
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

            return await query.ToListAsync();
        }


        public async Task EmprestarLivro(UserLivroModel emprestimo)
        {
            _context.userLivroEmprestimo.Add(emprestimo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LivroModel>> ObterTodosOsLivros()
        {
            return await _context.livroModels.ToListAsync();

        }
    }
}
