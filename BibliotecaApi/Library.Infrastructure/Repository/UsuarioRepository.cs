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

        public UsuarioRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioModelDTO>> ObterUsuariosCadastrados()
        {
            var userCadastrados = await _context.Registration.ToListAsync();
            var users = _mapper.Map<List<UsuarioModelDTO>>(userCadastrados);
            return users;
        }

        public async Task<UsuarioModelDTO> ObterUsuarioPorId(int id)
        {
            var usuario = await _context.Registration.FindAsync(id);

            if (usuario != null)
            {
                var usuarioDto = _mapper.Map<UsuarioModelDTO>(usuario);
                return usuarioDto;
            }
            else
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }
        }


        public async Task InserirUsuario(UsuarioModelDTO usuarioDto)
        {
            // Verifica se já existe um usuário com o mesmo nome
            bool usuarioExistente = await _context.Registration.AnyAsync(u => u.Name == usuarioDto.Name);

            if (!usuarioExistente)
            {
                // Mapeia UsuarioModelDTO para UsuarioModel
                var usuarioModel = _mapper.Map<UsuarioModel>(usuarioDto);

                // Adiciona o novo usuário ao contexto
                _context.Registration.Add(usuarioModel);

                // Salva as mudanças no banco de dados
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Usuário já cadastrado.");
            }
        }


        public async Task AtualizarUsuario(UsuarioModelDTO usuarioDto)
        {
            var attUser = _mapper.Map<UsuarioModel>(usuarioDto);
            _context.Entry(attUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirUsuario(int userId)
        {
            var usuario = await _context.Registration.FirstOrDefaultAsync(u => u.Id == userId);

            if (usuario != null)
            {
                _context.Registration.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }
        }

    }
}
