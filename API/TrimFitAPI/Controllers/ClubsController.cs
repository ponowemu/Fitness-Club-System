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
    public class ClubsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ClubsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Clubs
        [HttpGet]
        public IEnumerable<Club> GetClub()
        {
            return _context.Club
                .Include(a => a.Address)
                ;
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = await _context.Club
                .Include(a => a.Address)
                .FirstOrDefaultAsync(x => x.Club_Id == id);

            if (club == null)
            {
                return NotFound();
            }

            return Ok(club);
        }

        // PUT: api/Clubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub([FromRoute] int id, [FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != club.Club_Id)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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

        // POST: api/Clubs
        [HttpPost]
        public async Task<IActionResult> PostClub([FromBody] Club club)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Club.Add(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.Club_Id }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = await _context.Club.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Club.Remove(club);
            await _context.SaveChangesAsync();

            return Ok(club);
        }

        private bool ClubExists(int id)
        {
            return _context.Club.Any(e => e.Club_Id == id);
        }
    }
}