using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrimFitAPI.Models;

namespace TrimFitAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeClubsController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeeClubsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeClubs
        [HttpGet]
        public IEnumerable<EmployeeClub> GetEmployee_club()
        {
            return _context.Employee_club
                .Include(e=>e.Employee)
                .Include(c=>c.Club)
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

            var employeeClub = await _context.Employee_club
                .Include(e => e.Employee)
                .Include(c => c.Club)
                .FirstOrDefaultAsync(x=>x.Employee_Club_Id == id)
                ;

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

            if (id != employeeClub.Employee_Club_Id)
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

            _context.Employee_club.Add(employeeClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeClub", new { id = employeeClub.Employee_Club_Id }, employeeClub);
        }

        // DELETE: api/EmployeeClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeClub = await _context.Employee_club.FindAsync(id);
            if (employeeClub == null)
            {
                return NotFound();
            }

            _context.Employee_club.Remove(employeeClub);
            await _context.SaveChangesAsync();

            return Ok(employeeClub);
        }

        private bool EmployeeClubExists(int id)
        {
            return _context.Employee_club.Any(e => e.Employee_Club_Id == id);
        }
    }
}