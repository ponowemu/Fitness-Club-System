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
    public class ActivityClubsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ActivityClubsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/ActivityClubs
        [HttpGet]
        public IEnumerable<ActivityClub> GetActivity_club()
        {
            return _context.Activity_club;
        }

        // GET: api/ActivityClubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityClub = await _context.Activity_club.FindAsync(id);

            if (activityClub == null)
            {
                return NotFound();
            }

            return Ok(activityClub);
        }

        // PUT: api/ActivityClubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityClub([FromRoute] int id, [FromBody] ActivityClub activityClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityClub.Activity_Club_Id)
            {
                return BadRequest();
            }

            _context.Entry(activityClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityClubExists(id))
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

        // POST: api/ActivityClubs
        [HttpPost]
        public async Task<IActionResult> PostActivityClub([FromBody] ActivityClub activityClub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Activity_club.Add(activityClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityClub", new { id = activityClub.Activity_Club_Id }, activityClub);
        }

        // DELETE: api/ActivityClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityClub = await _context.Activity_club.FindAsync(id);
            if (activityClub == null)
            {
                return NotFound();
            }

            _context.Activity_club.Remove(activityClub);
            await _context.SaveChangesAsync();

            return Ok(activityClub);
        }

        private bool ActivityClubExists(int id)
        {
            return _context.Activity_club.Any(e => e.Activity_Club_Id == id);
        }
    }
}