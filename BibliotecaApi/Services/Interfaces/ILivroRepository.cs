using BibliotecaApi.Model;
using BibliotecaApi.Models;

namespace BibliotecaApi.Services.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<UserLivroModel>> ObterLivrosDisponiveis();
        Task<LivroModel> PesquisarLivros(string titulo, string autor, string genero);
        Task CadastrarLivro(LivroModel livro);
        Task DevolverLivro(int id);
        Task AtualizarLivros(LivroModel livro);
        Task EmprestarLivro(UserLivroModel emprestimo);
    }
}
