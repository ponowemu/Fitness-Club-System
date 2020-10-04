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
    public class EmployeeClubsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmployeeClubsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeClubs
        [HttpGet]
        public IEnumerable<EmployeeClub> GetEmployee_club()
        {
            return _context.EmployeeClub
                .Include(e => e.Employee)
                .Include(c => c.Club)
                ;
        }

        // GET: api/EmployeeClubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeClub = await _context.EmployeeClub
                .Include(e => e.Employee)
                    .ThenInclude(a => a.Address)
                .Include(c => c.Club)
                .Where(x => x.ClubId == id).ToListAsync();

            if (employeeClub == null)
            {
                return NotFound();
            }

            return Ok(employeeClub);
        }

        // PUT: api/EmployeeClubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeClub([FromRoute] int id, [FromBody] EmployeeClub employeeClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeClub.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeClubExists(id))
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

        // POST: api/EmployeeClubs
        [HttpPost]
        public async Task<IActionResult> PostEmployeeClub([FromBody] EmployeeClub employeeClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmployeeClub.Add(employeeClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeClub", new { id = employeeClub.Id }, employeeClub);
        }

        // DELETE: api/EmployeeClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeClub = await _context.EmployeeClub.FindAsync(id);
            if (employeeClub == null)
            {
                return NotFound();
            }

            _context.EmployeeClub.Remove(employeeClub);
            await _context.SaveChangesAsync();

            return Ok(employeeClub);
        }

        private bool EmployeeClubExists(int id)
        {
            return _context.EmployeeClub.Any(e => e.Id == id);
        }
    }
}