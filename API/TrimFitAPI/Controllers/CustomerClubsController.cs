using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrimFitAPI.Models;

namespace TrimFitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerClubsController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomerClubsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/CustomerClubs
        [HttpGet]
        public IEnumerable<CustomerClub> GetCustomer_club()
        {
            return _context.Customer_club
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

            var customerClub = await _context.Customer_club
                .Include(club => club.Club)
                .Include(cust => cust.Customer)
                .FirstOrDefaultAsync(x => x.Customer_Club_Id == id);

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

            if (id != customerClub.Customer_Club_Id)
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

            _context.Customer_club.Add(customerClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerClub", new { id = customerClub.Customer_Club_Id }, customerClub);
        }

        // DELETE: api/CustomerClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerClub = await _context.Customer_club.FindAsync(id);
            if (customerClub == null)
            {
                return NotFound();
            }

            _context.Customer_club.Remove(customerClub);
            await _context.SaveChangesAsync();

            return Ok(customerClub);
        }

        private bool CustomerClubExists(int id)
        {
            return _context.Customer_club.Any(e => e.Customer_Club_Id == id);
        }
    }
}