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
    public class VoucherTypesController : ControllerBase
    {
        private readonly ApiContext _context;

        public VoucherTypesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/VoucherTypes
        [HttpGet]
        public IEnumerable<VoucherType> GetVoucher_type()
        {
            return _context.Voucher_type;
        }

        // GET: api/VoucherTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucherType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucherType = await _context.Voucher_type.FindAsync(id);

            if (voucherType == null)
            {
                return NotFound();
            }

            return Ok(voucherType);
        }

        // PUT: api/VoucherTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucherType([FromRoute] int id, [FromBody] VoucherType voucherType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voucherType.Voucher_Type_Id)
            {
                return BadRequest();
            }

            _context.Entry(voucherType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherTypeExists(id))
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

        // POST: api/VoucherTypes
        [HttpPost]
        public async Task<IActionResult> PostVoucherType([FromBody] VoucherType voucherType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Voucher_type.Add(voucherType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucherType", new { id = voucherType.Voucher_Type_Id }, voucherType);
        }

        // DELETE: api/VoucherTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucherType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucherType = await _context.Voucher_type.FindAsync(id);
            if (voucherType == null)
            {
                return NotFound();
            }

            _context.Voucher_type.Remove(voucherType);
            await _context.SaveChangesAsync();

            return Ok(voucherType);
        }

        private bool VoucherTypeExists(int id)
        {
            return _context.Voucher_type.Any(e => e.Voucher_Type_Id == id);
        }
    }
}