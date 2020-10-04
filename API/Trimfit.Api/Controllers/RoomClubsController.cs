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
    public class RoomClubsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RoomClubsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RoomClubs
        [HttpGet]
        public IEnumerable<RoomClub> GetRoom_club()
        {
            return _context.RoomClub
                .Include(r=>r.Room)
                .Include(c=>c.Club);
        }

        // GET: api/RoomClubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomClub = await _context.RoomClub
                .Include(r => r.Room)
                .Include(c => c.Club)
                .FirstOrDefaultAsync(x=>x.Id == id)
                ;

            if (roomClub == null)
            {
                return NotFound();
            }

            return Ok(roomClub);
        }

        // PUT: api/RoomClubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomClub([FromRoute] int id, [FromBody] RoomClub roomClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomClub.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomClubExists(id))
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

        // POST: api/RoomClubs
        [HttpPost]
        public async Task<IActionResult> PostRoomClub([FromBody] RoomClub roomClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RoomClub.Add(roomClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomClub", new { id = roomClub.Id }, roomClub);
        }

        // DELETE: api/RoomClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomClub = await _context.RoomClub.FindAsync(id);
            if (roomClub == null)
            {
                return NotFound();
            }

            _context.RoomClub.Remove(roomClub);
            await _context.SaveChangesAsync();

            return Ok(roomClub);
        }

        private bool RoomClubExists(int id)
        {
            return _context.RoomClub.Any(e => e.Id == id);
        }
    }
}