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
    public class CustomerClubsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CustomerClubsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CustomerClubs
        [HttpGet]
        public IEnumerable<CustomerClub> GetCustomer_club()
        {
            return _context.CustomerClub
                .Include(club => club.Club)
                .Include(cust => cust.Customer)
                ;
        }

        // GET: api/CustomerClubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerClub = await _context.CustomerClub
                .Include(club => club.Club)
                .Include(cust => cust.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customerClub == null)
            {
                return NotFound();
            }

            return Ok(customerClub);
        }

        // PUT: api/CustomerClubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerClub([FromRoute] int id, [FromBody] CustomerClub customerClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerClub.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerClubExists(id))
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

        // POST: api/CustomerClubs
        [HttpPost]
        public async Task<IActionResult> PostCustomerClub([FromBody] CustomerClub customerClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerClub.Add(customerClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerClub", new { id = customerClub.Id }, customerClub);
        }

        // DELETE: api/CustomerClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerClub = await _context.CustomerClub.FindAsync(id);
            if (customerClub == null)
            {
                return NotFound();
            }

            _context.CustomerClub.Remove(customerClub);
            await _context.SaveChangesAsync();

            return Ok(customerClub);
        }

        private bool CustomerClubExists(int id)
        {
            return _context.CustomerClub.Any(e => e.Id == id);
        }
    }
}