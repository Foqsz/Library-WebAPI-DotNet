using BibliotecaApi.Library.Application.DTOs;
using BibliotecaApi.Library.Core.Model; 

namespace BibliotecaApi.Library.Application.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<UserLivroModelDTO>> ObterLivrosEmprestimoDisponiveis();
        Task<IEnumerable<UserLivroModelDTO>> PesquisarLivros(string? titulo, string? autor, string? genero);
        Task CadastrarLivro(LivroModelDTO livroDto);
        Task<IEnumerable<LivroModelDTO>> ObterTodosOsLivros();
        Task DevolverLivro(int id);
        Task AtualizarLivros(LivroModelDTO livroDto);
        Task EmprestarLivro(UserLivroModelDTO emprestimoDto);
    }
}
