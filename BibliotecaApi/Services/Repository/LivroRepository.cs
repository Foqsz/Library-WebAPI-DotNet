using BibliotecaApi.Model;
using BibliotecaApi.Models;
using BibliotecaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services.Repository
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
            var livro = await _context.livroModels.FindAsync(id);
            if (livro != null)
            {
                _context.livroModels.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task CadastrarLivro(LivroModel livro)
        {
            _context.livroModels.Add(livro);
            await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<UserLivroModel>> ObterLivrosDisponiveis()
        {
            return await _context.userLivroEmprestimo.ToListAsync();
        }

        public async Task<LivroModel> PesquisarLivros(string titulo, string autor, string genero)
        {
            return await _context.livroModels.FindAsync(titulo, autor, genero);
        }

        public async Task EmprestarLivro(UserLivroModel emprestimo)
        {
            _context.userLivroEmprestimo.Add(emprestimo);
             await _context.SaveChangesAsync();
        }
    }
}
