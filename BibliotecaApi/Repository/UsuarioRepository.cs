using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaApi.Repository
{
    public class UsuarioRepository : IUsuarioServiceDTO
    {
        private readonly LibraryContext _context;

        public UsuarioRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceUsuarioDTO>> ObterUsuariosCadastrados()
        {
            return await _context.Registration.ToListAsync();
        }

        public async Task<ServiceUsuarioDTO> ObterUsuarioPorId(int id)
        {
            return await _context.Registration.FindAsync(id);
        }

        public async Task InserirUsuario(ServiceUsuarioDTO usuario)
        {
            _context.Registration.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarUsuario(ServiceUsuarioDTO usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirUsuario(int id)
        {
            var usuario = await _context.Registration.FindAsync(id);
            if (usuario != null)
            {
                _context.Registration.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
