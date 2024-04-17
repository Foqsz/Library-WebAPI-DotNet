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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryRegistration>>> GetRegistration()
        {
            return await _context.Registration.ToListAsync();
        }

        // GET: api/LibraryRegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryRegistration>> GetLibraryRegistration(string id)
        {
            var libraryRegistration = await _context.Registration.FindAsync(id);

            if (libraryRegistration == null)
            {
                return NotFound();
            }

            return libraryRegistration;
        }

        // PUT: api/LibraryRegistrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryRegistration(string id, LibraryRegistration libraryRegistration)
        {
            if (id != libraryRegistration.Name)
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
                if (!LibraryRegistrationExists(id))
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
        [HttpPost]
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryRegistration(string id)
        {
            var libraryRegistration = await _context.Registration.FindAsync(id);
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
