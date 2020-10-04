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
    public class VoucherTypesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VoucherTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/VoucherTypes
        [HttpGet]
        public IEnumerable<VoucherType> GetVoucher_type()
        {
            return _context.VoucherType;
        }

        // GET: api/VoucherTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucherType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucherType = await _context.VoucherType.FindAsync(id);

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

            if (id != voucherType.Id)
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

            _context.VoucherType.Add(voucherType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucherType", new { id = voucherType.Id }, voucherType);
        }

        // DELETE: api/VoucherTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucherType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucherType = await _context.VoucherType.FindAsync(id);
            if (voucherType == null)
            {
                return NotFound();
            }

            _context.VoucherType.Remove(voucherType);
            await _context.SaveChangesAsync();

            return Ok(voucherType);
        }

        private bool VoucherTypeExists(int id)
        {
            return _context.VoucherType.Any(e => e.Id == id);
        }
    }
}