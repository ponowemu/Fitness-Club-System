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
    public class PositionClubsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PositionClubsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PositionClubs
        [HttpGet]
        public IEnumerable<PositionClub> GetPosition_club()
        {
            return _context.PositionClub
                .Include(p=>p.Position)
                .Include(c=>c.Club);
        }

        // GET: api/PositionClubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var positionClub = await _context.PositionClub
                .Include(p => p.Position)
                .Include(c => c.Club)
                .FirstOrDefaultAsync(x=>x.Id == id); 

            if (positionClub == null)
            {
                return NotFound();
            }

            return Ok(positionClub);
        }

        // PUT: api/PositionClubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionClub([FromRoute] int id, [FromBody] PositionClub positionClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != positionClub.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionClubExists(id))
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

        // POST: api/PositionClubs
        [HttpPost]
        public async Task<IActionResult> PostPositionClub([FromBody] PositionClub positionClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PositionClub.Add(positionClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionClub", new { id = positionClub.Id }, positionClub);
        }

        // DELETE: api/PositionClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var positionClub = await _context.PositionClub.FindAsync(id);
            if (positionClub == null)
            {
                return NotFound();
            }

            _context.PositionClub.Remove(positionClub);
            await _context.SaveChangesAsync();

            return Ok(positionClub);
        }

        private bool PositionClubExists(int id)
        {
            return _context.PositionClub.Any(e => e.Id == id);
        }
    }
}