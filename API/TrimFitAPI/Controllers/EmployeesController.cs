using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrimFitAPI.Models;

namespace TrimFitAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IHostingEnvironment _env;

        public EmployeesController(ApiContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee
                .Include(e => e.Address)
                ;
        }
        [HttpGet("{employeeId}/Activities")]
        public async Task<IActionResult> GetActivities(int employeeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ac = await _context.Activity
                .Where(a => a.Employee_Id.Contains(employeeId))
                .ToListAsync();

            if (ac.Count > 0)
            {
                return Ok(ac);
            }
            else
                return NotFound();
        }
        //POST: api/Upload
        [HttpPost("/upload")]
        public async Task<IActionResult> UploadPhoto([FromForm(Name = "formFile")]IFormFile formFile)
        {
            var filePaths = string.Empty;

            if (formFile.Length > 0)
            {
                // full path to file in temp location
                var ext = formFile.FileName.Split('.');
                var filePath = Path.Combine(
                    _env.WebRootPath,
                    "images",
                    String.Join('.', Guid.NewGuid(), ext[1]));
                filePaths = filePath;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            else
                return NotFound();

            return Ok(filePaths);
        }
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee
                .Include(e => e.Address)
                .FirstOrDefaultAsync(x => x.Employee_Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Employee_Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;
            _context.Entry(employee.Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Employee_Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Employee_Id == id);
        }
    }
}