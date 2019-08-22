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
    public class TimetableActivitiesController : ControllerBase
    {
        private readonly ApiContext _context;

        public TimetableActivitiesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Timetables
        [HttpGet]
        public IEnumerable<TimetableActivity> GetTimetableActivities()
        {
            return _context.Timetable_activity
                .Include(e => e.Employee)
                .Include(a => a.Activity)
                .Include(t => t.Timetable)
                .Include(r => r.Room)
                ;
        }

        // GET: api/Timetables/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimetableActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timetable = await _context.Timetable_activity
                .Include(a => a.Activity)
                .Include(t => t.Timetable)
                .Include(r => r.Room)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(x => x.Timetable_Activity_Id == id);
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

            if (id != timetableActivity.Timetable_Activity_Id)
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

            _context.Timetable_activity.Add(timetableActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimetableActivity", new { id = timetableActivity.Timetable_Activity_Id }, timetableActivity);
        }

        // DELETE: api/Timetables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimetableActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timetable = await _context.Timetable_activity.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }

            _context.Timetable_activity.Remove(timetable);
            await _context.SaveChangesAsync();

            return Ok(timetable);
        }

        private bool TimetableExists(int id)
        {
            return _context.Timetable_activity.Any(e => e.Timetable_Activity_Id == id);
        }
    }
}