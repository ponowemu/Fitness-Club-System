using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trimfit.Domain;
using Trimfit.Domain.Models;

namespace Trimfit.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RegistrationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Registrations
        [HttpGet]
        public IEnumerable<Registration> GetRegistration()
        {
            return _context.Registration
                .Include(t=>t.TimetableActivity)
                .Include(p=>p.Payment)
                .Include(c=>c.Customer)
                ;
        }

        // GET: api/Registrations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _context.Registration
                .Include(t => t.TimetableActivity)
                .Include(p => p.Payment)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(x=>x.Id == id)
                ;

            if (registration == null)
            {
                return NotFound();
            }

            return Ok(registration);
        }

        // PUT: api/Registrations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration([FromRoute] int id, [FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registration.Id)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
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

        // POST: api/Registrations
        [HttpPost]
        public async Task<IActionResult> PostRegistration([FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Registration.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = registration.Id }, registration);
        }

        // DELETE: api/Registrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _context.Registration.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registration.Remove(registration);
            await _context.SaveChangesAsync();

            return Ok(registration);
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registration.Any(e => e.Id == id);
        }
    }
}