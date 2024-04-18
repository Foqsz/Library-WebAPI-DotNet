using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaApi.Models;

namespace BibliotecaApi.Repositorios.Interfaces
{
    public interface IUsuarioServiceDTO
    {
        Task<IEnumerable<ServiceUsuarioModel>> ObterUsuariosCadastrados();
        Task<ServiceUsuarioModel> ObterUsuarioPorId(int id);
        Task InserirUsuario(ServiceUsuarioModel usuario);
        Task AtualizarUsuario(ServiceUsuarioModel usuario);
        Task ExcluirUsuario(int id);
    }
}
