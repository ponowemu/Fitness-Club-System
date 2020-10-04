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
    public class ActivityClubsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ActivityClubsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ActivityClubs
        [HttpGet]
        public IEnumerable<ActivityClub> GetActivity_club()
        {
            return _context.ActivityClub
                .Include(a => a.Activity)
                .Include(c => c.Club)
                ;
        }

        // GET: api/ActivityClubs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityClub = await _context.ActivityClub
                .Include(a => a.Activity)
                .Include(c => c.Club)
                .FirstOrDefaultAsync(x => x.Id == id);

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

            if (id != activityClub.Id)
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

            _context.ActivityClub.Add(activityClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityClub", new { id = activityClub.Id }, activityClub);
        }

        // DELETE: api/ActivityClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityClub([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityClub = await _context.ActivityClub.FindAsync(id);
            if (activityClub == null)
            {
                return NotFound();
            }

            _context.ActivityClub.Remove(activityClub);
            await _context.SaveChangesAsync();

            return Ok(activityClub);
        }

        private bool ActivityClubExists(int id)
        {
            return _context.ActivityClub.Any(e => e.Id == id);
        }
    }
}