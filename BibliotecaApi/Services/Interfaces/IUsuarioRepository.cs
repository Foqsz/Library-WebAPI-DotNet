using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaApi.Models;

namespace BibliotecaApi.Repositorios.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioModel>> ObterUsuariosCadastrados();
        Task<UsuarioModel> ObterUsuarioPorId(int id);
        Task InserirUsuario(UsuarioModel usuario);
        Task AtualizarUsuario(UsuarioModel usuario);
        Task ExcluirUsuario(int id);
    }
}
