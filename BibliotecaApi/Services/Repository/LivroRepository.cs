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
            throw new NotImplementedException();
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
        
        public async Task EmprestarLivro(LivroModel livro)
        {
            _context.livroModels.Add(livro);
            await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<LivroModel>> ObterLivrosDisponiveis()
        {
            return await _context.livroModels.ToListAsync();
        }

        public async Task<LivroModel> PesquisarLivros(string titulo, string autor, string genero)
        {
            return await _context.livroModels.FindAsync(titulo, autor, genero);
        }
    }
}
