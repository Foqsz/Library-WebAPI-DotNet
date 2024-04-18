using BibliotecaApi.Models;
using BibliotecaApi.Repositorios.Interfaces;
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

        public async Task<IEnumerable<ServiceUsuarioModel>> ObterUsuariosCadastrados()
        {
            return await _context.Registration.ToListAsync();
        }

        public async Task<ServiceUsuarioModel> ObterUsuarioPorId(int id)
        {
            return await _context.Registration.FindAsync(id);
        }

        public async Task InserirUsuario(ServiceUsuarioModel usuario)
        {
            _context.Registration.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarUsuario(ServiceUsuarioModel usuario)
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
