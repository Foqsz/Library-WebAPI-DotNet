using Amazon.Auth.AccessControlPolicy;
using BibliotecaApi.Models;
using BibliotecaApi.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BibliotecaApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibraryContext _context;

        public UsuarioRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioModel>> ObterUsuariosCadastrados()
        {
            return await _context.Registration.ToListAsync();
        }

        public async Task<UsuarioModel> ObterUsuarioPorId(int id)
        {
            return await _context.Registration.FindAsync(id);
        }

        public async Task InserirUsuario(UsuarioModel usuario)
        {
            _context.Registration.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarUsuario(UsuarioModel usuario)
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
        public async Task ObterUsuarioPorNomeESenha(string name, string senha)
        {
            var userSenha = await _context.Registration.FirstOrDefaultAsync(u => u.Name == name && u.Senha == senha);
        }

    }
}
