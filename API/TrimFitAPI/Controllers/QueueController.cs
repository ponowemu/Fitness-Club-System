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
    public class QueueController : ControllerBase
    {
        private readonly ApiContext _context;

        public QueueController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/GetLastRequest
        [HttpGet]
        public async Task<Queue> GetLastRequest()
        {
            var result = await _context.Queues
                .OrderByDescending(q => q.Created)
                    .Include(c => c.Customer)
                        .ThenInclude(v => v.Vouchers)
                            .ThenInclude(vt => vt.Voucher.VoucherType)
                .FirstOrDefaultAsync();

            if (result != null)
            {
                _context.Queues.Remove(result);
                await _context.SaveChangesAsync();

                return result;
            }
            Response.StatusCode = 404;
            return null;
        }
     
        // POST: api/Positions
        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] Queue queue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Queues.Add(queue);
            await _context.SaveChangesAsync();

            return Ok(queue);
        }

    }
}