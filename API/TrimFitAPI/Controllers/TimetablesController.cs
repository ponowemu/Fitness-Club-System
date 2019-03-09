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
    public class TimetablesController : ControllerBase
    {
        private readonly ApiContext _context;

        public TimetablesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Timetables
        [HttpGet]
        public IEnumerable<Timetable> GetTimetable()
        {
            return _context.Timetable;
        }

        // GET: api/Timetables/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimetable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timetable = await _context.Timetable.FindAsync(id);

            if (timetable == null)
            {
                return NotFound();
            }

            return Ok(timetable);
        }

        // PUT: api/Timetables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimetable([FromRoute] int id, [FromBody] Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timetable.Timetable_Id)
            {
                return BadRequest();
            }

            _context.Entry(timetable).State = EntityState.Modified;

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
        public async Task<IActionResult> PostTimetable([FromBody] Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Timetable.Add(timetable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimetable", new { id = timetable.Timetable_Id }, timetable);
        }

        // DELETE: api/Timetables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimetable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timetable = await _context.Timetable.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }

            _context.Timetable.Remove(timetable);
            await _context.SaveChangesAsync();

            return Ok(timetable);
        }

        private bool TimetableExists(int id)
        {
            return _context.Timetable.Any(e => e.Timetable_Id == id);
        }
    }
}