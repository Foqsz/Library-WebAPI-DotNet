using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaApi.Models;

namespace BibliotecaApi.Repository
{
    public interface IUsuarioServiceDTO
    {
        Task<IEnumerable<ServiceUsuarioDTO>> ObterUsuariosCadastrados();
        Task<ServiceUsuarioDTO> ObterUsuarioPorId(int id);
        Task InserirUsuario(ServiceUsuarioDTO usuario);
        Task AtualizarUsuario(ServiceUsuarioDTO usuario);
        Task ExcluirUsuario(int id);
    }
}
