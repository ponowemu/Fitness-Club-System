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
    public class VoucherCustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public VoucherCustomersController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/VoucherCustomers
        [HttpGet]
        public IEnumerable<VoucherCustomer> GetVoucherCustomer()
        {
            return _context.VoucherCustomer
                .Include(v=>v.Voucher)
                .Include(c=>c.Customer)
                ;
        }

        // GET: api/VoucherCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucherCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucherCustomer = await _context.VoucherCustomer
                .Include(v => v.Voucher)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(x=>x.Voucher_Customer_Id == id);

            if (voucherCustomer == null)
            {
                return NotFound();
            }

            return Ok(voucherCustomer);
        }

        // PUT: api/VoucherCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucherCustomer([FromRoute] int id, [FromBody] VoucherCustomer voucherCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voucherCustomer.Voucher_Customer_Id)
            {
                return BadRequest();
            }

            _context.Entry(voucherCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherCustomerExists(id))
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

        // POST: api/VoucherCustomers
        [HttpPost]
        public async Task<IActionResult> PostVoucherCustomer([FromBody] VoucherCustomer voucherCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VoucherCustomer.Add(voucherCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucherCustomer", new { id = voucherCustomer.Voucher_Customer_Id }, voucherCustomer);
        }

        // DELETE: api/VoucherCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucherCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucherCustomer = await _context.VoucherCustomer.FindAsync(id);
            if (voucherCustomer == null)
            {
                return NotFound();
            }

            _context.VoucherCustomer.Remove(voucherCustomer);
            await _context.SaveChangesAsync();

            return Ok(voucherCustomer);
        }

        private bool VoucherCustomerExists(int id)
        {
            return _context.VoucherCustomer.Any(e => e.Voucher_Customer_Id == id);
        }
    }
}