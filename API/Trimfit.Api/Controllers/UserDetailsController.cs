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
    public class UserDetailsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UserDetailsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails
        [HttpGet]
        public IEnumerable<UserDetail> GetUserDetail()
        {
            return _context.UserDetail;
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetail = await _context.UserDetail.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        // PUT: api/UserDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail([FromRoute] int id, [FromBody] UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
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

        // POST: api/UserDetails
        [HttpPost]
        public async Task<IActionResult> PostUserDetail([FromBody] UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserDetail.Add(userDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetail", new { id = userDetail.Id }, userDetail);
        }

        // DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetail = await _context.UserDetail.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            _context.UserDetail.Remove(userDetail);
            await _context.SaveChangesAsync();

            return Ok(userDetail);
        }

        private bool UserDetailExists(int id)
        {
            return _context.UserDetail.Any(e => e.Id == id);
        }
    }
}