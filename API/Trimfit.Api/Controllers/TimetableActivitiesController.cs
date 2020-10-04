using System;
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
    public class TimetableActivitiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TimetableActivitiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Timetables
        [HttpGet]
        public IEnumerable<TimetableActivity> GetTimetableActivities([FromQuery] bool incoming = false, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null, [FromQuery] int? timetable = null, [FromQuery] bool related = false)
        {
            var predicate = PredicateHelper.True<TimetableActivity>();
            if (from != null)
                predicate = predicate.And(x => x.Starttime >= from);
            else if (incoming)
                predicate = predicate.And(x => x.Starttime >= DateTime.Today);
            if (to != null)
                predicate = predicate.And(x => x.Starttime <= to);
            if (timetable != null)
                predicate = predicate.And(x => x.TimetableId == timetable);

            if (related == true)
            {
                return _context.TimetableActivity
                    .Include(e => e.Employee)
                    .Include(a => a.Activity)
                    .Include(t => t.Timetable)
                    .Include(r => r.Room)
                    .Where(predicate);
            }
            else
            {
                return _context.TimetableActivity
                    .Where(predicate);
            }
        }

        // GET: api/Timetables/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimetableActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timetable = await _context.TimetableActivity
                .Include(a => a.Activity)
                .Include(t => t.Timetable)
                .Include(r => r.Room)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);

            //var timetable = await _context.Timetable_activity.FindAsync(id);

            if (timetable == null)
            {
                return NotFound();
            }

            return Ok(timetable);
        }

        // PUT: api/Timetables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimetableActivity([FromRoute] int id, [FromBody] TimetableActivity timetableActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timetableActivity.Id)
            {
                return BadRequest();
            }

            _context.Entry(timetableActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimetableExists(id))
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

        // POST: api/Timetables
        [HttpPost]
        public async Task<IActionResult> PostTimetableActivity([FromBody] TimetableActivity timetableActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TimetableActivity.Add(timetableActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimetableActivity", new { id = timetableActivity.Id }, timetableActivity);
        }

        // DELETE: api/Timetables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimetableActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timetable = await _context.TimetableActivity.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }

            _context.TimetableActivity.Remove(timetable);
            await _context.SaveChangesAsync();

            return Ok(timetable);
        }

        private bool TimetableExists(int id)
        {
            return _context.TimetableActivity.Any(e => e.Id == id);
        }
    }
}