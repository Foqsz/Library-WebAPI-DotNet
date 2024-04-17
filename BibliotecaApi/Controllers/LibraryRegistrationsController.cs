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
        public async Task<ActionResult<IEnumerable<LibraryRegistration>>> GetRegistration()
        {
            return await _context.Registration.ToListAsync();
        }

        // GET: api/LibraryRegistrations/5
        [HttpGet("PesquisaPorNome")]
        public async Task<ActionResult<LibraryRegistration>> GetLibraryRegistration(string name)
        {
            var libraryRegistration = await _context.Registration.FindAsync(name);

            if (libraryRegistration == null)
            {
                return NotFound();
            }

            return libraryRegistration;
        }

        // PUT: api/LibraryRegistrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditarSuasInformacoes")]
        public async Task<IActionResult> PutLibraryRegistration(string name, LibraryRegistration libraryRegistration)
        {
            if (name != libraryRegistration.Name)
            {
                return BadRequest();
            }

            _context.Entry(libraryRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryRegistrationExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LibraryRegistrations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Cadastramento")]
        public async Task<ActionResult<LibraryRegistration>> PostLibraryRegistration(LibraryRegistration libraryRegistration)
        {
            _context.Registration.Add(libraryRegistration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LibraryRegistrationExists(libraryRegistration.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetLibraryRegistration), new { id = libraryRegistration.Name }, libraryRegistration);
        }

        // DELETE: api/LibraryRegistrations/5
        [HttpDelete("ManipulacaoDeUsuario")]
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
    }
}
