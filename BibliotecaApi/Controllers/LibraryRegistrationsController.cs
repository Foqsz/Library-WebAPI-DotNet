using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Models;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryRegistrationsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LibraryRegistrationsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/LibraryRegistrations
        [HttpGet("UsuariosCadastrados")]
        public async Task<ActionResult<IEnumerable<LibraryRegistrationDTO>>> GetRegistration()
        {
            return await _context.Registration
                .Select(x => LibraryIsDTO(x))
                .ToListAsync();
        }

        // GET: api/LibraryRegistrations/5
        [HttpGet("{name} PesquisaPorNome")]
        public async Task<ActionResult<LibraryRegistrationDTO>> GetLibraryRegistration(string name)
        {
            var libraryRegistration = await _context.Registration.FindAsync(name);

            if (libraryRegistration == null)
            {
                return NotFound();
            }

            return LibraryIsDTO(libraryRegistration);
        }

        // PUT: api/LibraryRegistrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name} EditarSuasInformacoes")]
        public async Task<IActionResult> PutLibraryRegistration(string name, LibraryRegistrationDTO libraryDTO)
        {
            if (name != libraryDTO.Name)
            {
                return BadRequest();
            }

            var libraryRegistration = await _context.Registration.FindAsync(name);
            if (libraryRegistration == null)
            {
                return NotFound();
            }

            libraryRegistration.Name = libraryDTO.Name;
            libraryRegistration.Email = libraryDTO.Email;
            libraryRegistration.Senha = libraryDTO.Senha;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!LibraryRegistrationExists(name))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/LibraryRegistrations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Cadastramento")]
        public async Task<ActionResult<LibraryRegistrationDTO>> PostLibraryRegistration(LibraryRegistrationDTO libraryIsDTO)
        {
            var libraryRegistration = new LibraryRegistration()
            {
                Name = libraryIsDTO.Name,
                Email = libraryIsDTO.Email,
                Senha = libraryIsDTO.Senha
            };

            _context.Registration.Add(libraryRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLibraryRegistration),
                new { name = libraryRegistration.Name },
                LibraryIsDTO(libraryRegistration));
        }

        // DELETE: api/LibraryRegistrations/5
        [HttpDelete("{name} ManipulacaoDeUsuario")]
        public async Task<IActionResult> DeleteLibraryRegistration(string name)
        {
            var libraryRegistration = await _context.Registration.FindAsync(name);
            if (libraryRegistration == null)
            {
                return NotFound();
            }

            _context.Registration.Remove(libraryRegistration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibraryRegistrationExists(string id)
        {
            return _context.Registration.Any(e => e.Name == id);
        }

        private static LibraryRegistrationDTO LibraryIsDTO(LibraryRegistration libraryRegistration) =>
            new LibraryRegistrationDTO
            {
                Name = libraryRegistration.Name,
                Email = libraryRegistration.Email,
                Senha = libraryRegistration.Senha
            };
    }
}
