using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BibliotecaApi.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuario;

        public UsuarioController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            var usuarios = await _usuario.ObterUsuariosCadastrados();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioPorId(int id)
        {
            var usuario = await _usuario.ObterUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
            return Ok(usuario);
        }

        [HttpPut("{id}")]
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

        [HttpPost]
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

        [HttpDelete("{id}")]
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
