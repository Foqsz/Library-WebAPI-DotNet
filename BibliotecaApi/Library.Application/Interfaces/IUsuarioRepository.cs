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
        Task InserirUsuario(UsuarioModel usuario);
        Task AtualizarUsuario(UsuarioModel usuario);
        Task ExcluirUsuario(int id);
    }
}
