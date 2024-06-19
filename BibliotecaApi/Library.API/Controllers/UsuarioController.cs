using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using BibliotecaApi.Library.Application.Interfaces;
using BibliotecaApi.Library.Core.Model;
using BibliotecaApi.Library.Application.DTOs;
using AutoMapper;

namespace BibliotecaApi.Library.API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuario;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuario, IMapper mapper)
        {
            _usuario = usuario;
            _mapper = mapper;
        }

        //GET: /api/Usuario/ListarUsuarios
        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioModelDTO>>> GetUsuarios()
        {
            var usuarios = await _usuario.ObterUsuariosCadastrados();

            var usuariosDto = _mapper.Map<IEnumerable<UsuarioModel>>(usuarios);

            return Ok(usuariosDto);
        }

        //GET: /api/Usuario/PesquisarUsuario
        [HttpGet("PesquisarUsuario/{id}")]
        public async Task<ActionResult<UsuarioModelDTO>> GetUsuarioPorId(int id)
        {
            var usuario = await _usuario.ObterUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            var usuarioDto = _mapper.Map<IEnumerable<UsuarioModel>>(usuario);

            return Ok(usuarioDto);
        }

        //GET: /api/Usuario/AtualizarUsuario/id
        [HttpPut("AtualizarUsuario/{id}")]
        public async Task<ActionResult<UsuarioModelDTO>> AtualizarUsuario(int id, UsuarioModelDTO usuario)
        {
            if (id == null)
            {
                return BadRequest("ID do usuário na rota não corresponde ao ID do usuário no corpo da requisição.");
            }
            try
            {
                await _usuario.AtualizarUsuario(usuario);
                return Ok(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar o usuário.");
            }
        }

        //GET: /api/Usuario/NovoUsuario
        [HttpPost("NovoUsuario")]
        public async Task<ActionResult<UsuarioModelDTO>> InserirUsuario(UsuarioModelDTO novoUsuario)
        {
            try
            {
                await _usuario.InserirUsuario(novoUsuario); 
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir o usuário.");
            }
            return Ok(novoUsuario);
        }

        //GET: /api/Usuario/ExcluirUsuario/id
        [HttpDelete("ExcluirUsuario/{id}")]
        public async Task<ActionResult<UsuarioModelDTO>> ExcluirUsuario(int id)
        {
            try
            {
                await _usuario.ExcluirUsuario(id);
                return Ok($"Usuário com ID {id} excluído com sucesso.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar excluir o usuário.");
            }
        }
    }
}
