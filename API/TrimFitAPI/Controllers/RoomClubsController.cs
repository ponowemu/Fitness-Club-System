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
    public class RoomClubsController : ControllerBase
    {
        private readonly ApiContext _context;

        public RoomClubsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/RoomClubs
        [HttpGet]
        public IEnumerable<RoomClub> GetRoom_club()
        {
            return _context.Room_club
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

            var roomClub = await _context.Room_club
                .Include(r => r.Room)
                .Include(c => c.Club)
                .FirstOrDefaultAsync(x=>x.Room_Club_Id == id)
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

            if (id != roomClub.Room_Club_Id)
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

            _context.Room_club.Add(roomClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomClub", new { id = roomClub.Room_Club_Id }, roomClub);
        }

        // DELETE: api/RoomClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomClub = await _context.Room_club.FindAsync(id);
            if (roomClub == null)
            {
                return NotFound();
            }

            _context.Room_club.Remove(roomClub);
            await _context.SaveChangesAsync();

            return Ok(roomClub);
        }

        private bool RoomClubExists(int id)
        {
            return _context.Room_club.Any(e => e.Room_Club_Id == id);
        }
    }
}