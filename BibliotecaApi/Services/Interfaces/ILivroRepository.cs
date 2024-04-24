using BibliotecaApi.Model;
using BibliotecaApi.Models;

namespace BibliotecaApi.Services.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroModel>> ObterLivrosDisponiveis();
        Task<LivroModel> PesquisarLivros(string titulo, string autor, string genero);
        Task EmprestarLivro(LivroModel livro);
        Task DevolverLivro(int id);
        Task AtualizarLivros(LivroModel livro);
    }
}
