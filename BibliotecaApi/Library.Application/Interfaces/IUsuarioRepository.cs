using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaApi.Library.Application.DTOs;
using BibliotecaApi.Library.Core.Model;

namespace BibliotecaApi.Library.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioModelDTO>> ObterUsuariosCadastrados();
        Task<UsuarioModelDTO> ObterUsuarioPorId(int id);
        Task InserirUsuario(UsuarioModelDTO usuario);
        Task AtualizarUsuario(UsuarioModelDTO usuario);
        Task ExcluirUsuario(int id);
    }
}
