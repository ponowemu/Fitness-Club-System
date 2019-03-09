using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrimFitAPI.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApiContext _context;

        public PaymentsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public IEnumerable<Payment> GetPayment()
        {
            return _context.Payment;
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = await _context.Payment.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment([FromRoute] int id, [FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.Payment_Id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.Payment_Id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Payment_Id == id);
        }
    }
}