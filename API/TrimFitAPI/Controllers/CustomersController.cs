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
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomer()
        {
            return _context.Customer
                .Include(a => a.Address)
                ;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer
                .Include(a => a.Address)
                .Include(v => v.Vouchers)
                    .ThenInclude(b => b.Voucher)
                        .ThenInclude(c => c.VoucherType)
                .FirstOrDefaultAsync(x => x.Customer_Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet("{id}/Registrations")]
        public async Task<IActionResult> GetRegistrations([FromRoute] int id, [FromQuery] bool incoming = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var predicate = PredicateBuilder.True<Registration>();
            if (incoming)
                predicate = predicate.And(x => x.TimetableActivity.Timetable_Activity_Starttime >= DateTime.Today);

            var Registration = await _context.Registration
                .Include(t => t.TimetableActivity)
                .Include(p => p.Payment)
                .Where(predicate)
                .FirstOrDefaultAsync(x => x.Customer_Id == id);

            if (Registration == null)
            {
                return NotFound();
            }

            return Ok(Registration);
        }

        [HttpGet("{id}/Reservations")]
        public async Task<IActionResult> GetReservations([FromRoute] int id, [FromQuery] bool incoming = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var predicate = PredicateBuilder.True<Reservation>();
            if (incoming)
                predicate = predicate.And(x => x.Reservation_From >= DateTime.Today);

            var Registration = await _context.Reservation
                .Include(s => s.Service)
                .Include(p => p.Payment)
                .Include(c => c.Club)
                .Where(predicate)
                .FirstOrDefaultAsync(x => x.Customer_Id == id);

            if (Registration == null)
            {
                return NotFound();
            }

            return Ok(Registration);
        }
        [HttpGet("Details")]
        public async Task<IEnumerable<Customer>> GetDetails()
        {
            var customer = await _context.Customer
                .Include(a => a.Address)
                .Include(v => v.Vouchers)
                    .ThenInclude(vr => vr.Voucher)
                .ToListAsync();

            if (customer == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return customer;
        }
        [HttpGet("{id}/Vouchers")]
        public async Task<IActionResult> GetVouchers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vouchers = await _context.VoucherCustomer
                .Include(v => v.Voucher)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(x => x.Customer_Id == id);

            if (vouchers == null)
            {
                return NotFound();
            }

            return Ok(vouchers);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Customer_Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            _context.Entry(customer.Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Customer_Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Customer_Id == id);
        }
    }
}