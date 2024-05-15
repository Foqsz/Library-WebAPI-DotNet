using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BibliotecaApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuario;

        public UsuarioController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        //GET: /api/Usuario/ListarUsuarios
        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            var usuarios = await _usuario.ObterUsuariosCadastrados();
            return Ok(usuarios);
        }

        //GET: /api/Usuario/PesquisarUsuario
        [HttpGet("PesquisarUsuario/{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioPorId(int id)
        {
            var usuario = await _usuario.ObterUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
            return Ok(usuario);
        }

        //GET: /api/Usuario/AtualizarUsuario/id
        [HttpPut("AtualizarUsuario/{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, UsuarioModel usuario)
        {
            if (id != usuario.Id)
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
        public async Task<IActionResult> InserirUsuario(UsuarioModel novoUsuario)
        {
            try
            {
                await _usuario.InserirUsuario(novoUsuario);
                return CreatedAtAction(nameof(GetUsuarioPorId), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir o usuário.");
            }
        }

        //GET: /api/Usuario/ExcluirUsuario/id
        [HttpDelete("ExcluirUsuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
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
