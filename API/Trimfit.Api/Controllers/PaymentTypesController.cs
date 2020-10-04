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
    public class PaymentTypesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PaymentTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PaymentTypes
        [HttpGet]
        public IEnumerable<PaymentType> GetPayment_type()
        {
            return _context.PaymentType;
        }

        // GET: api/PaymentTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentType = await _context.PaymentType.FindAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return Ok(paymentType);
        }

        // PUT: api/PaymentTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType([FromRoute] int id, [FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // POST: api/PaymentTypes
        [HttpPost]
        public async Task<IActionResult> PostPaymentType([FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentType.Add(paymentType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentType", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentType = await _context.PaymentType.FindAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _context.PaymentType.Remove(paymentType);
            await _context.SaveChangesAsync();

            return Ok(paymentType);
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.Id == id);
        }
    }
}