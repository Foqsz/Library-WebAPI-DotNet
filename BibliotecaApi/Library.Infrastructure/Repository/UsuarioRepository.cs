using AutoMapper;
using BibliotecaApi.Library.Application.DTOs;
using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using BibliotecaApi.Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BibliotecaApi.Library.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public UsuarioRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioModelDTO>> ObterUsuariosCadastrados()
        { 
            return await _context.Registration.Select(u => new UsuarioModelDTO { Id = u.Id, Email = u.Email, Name = u.Name }).ToListAsync();
        }

        public async Task<UsuarioModelDTO> ObterUsuarioPorId(int id)
        {
            return await _context.Registration.Where(u => u.Id == id).Select(u => new UsuarioModelDTO { Id = u.Id, Email = u.Email, Name = u.Name }).FirstOrDefaultAsync();
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
