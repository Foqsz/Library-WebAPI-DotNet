using BibliotecaApi.Model;
using BibliotecaApi.Models;

namespace BibliotecaApi.Services.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<UserLivroModel>> ObterLivrosEmprestimoDisponiveis();
        Task<IEnumerable<UserLivroModel>> PesquisarLivros(string? titulo, string? autor, string? genero);
        Task CadastrarLivro(LivroModel livro);
        Task<IEnumerable<LivroModel>> ObterTodosOsLivros(); 
        Task DevolverLivro(int id);
        Task AtualizarLivros(LivroModel livro); 
        Task EmprestarLivro(UserLivroModel emprestimo);
    }
}
