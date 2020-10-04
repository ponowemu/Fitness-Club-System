using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trimfit.Domain;
using Trimfit.Domain.Models;

namespace Trimfit.WebApi.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            sha1.Dispose();
            return HexStringFromBytes(hashBytes);
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }


        [HttpGet("{id}/Customer")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Customer = await _context.Customer
                .Include(a=>a.Address)
                .Include(u=>u.User)
                .FirstOrDefaultAsync(x=>x.UserId == id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Ok(Customer);
        }

        [HttpGet("{id}/Employee")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Employee = await _context.Employee
                .Include(a => a.Address)
                .Include(u => u.User)
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (Employee == null)
            {
                return NotFound();
            }

            return Ok(Employee);
        }

        //logowanie
        [AllowAnonymous]
        [HttpPost("[action]/")]
        public async Task<IActionResult> Login(UserLogin temp_user)
        {
            //var hash_password = SHA1HashStringForUTF8String(temp_user.User_password);
            var user = await _context.User.Where(u => u.Login == temp_user.Login && u.Password.ToUpper() == temp_user.Password).SingleOrDefaultAsync();
            user = await Authenticate(user);
            if (user != null)
            {
                return Ok(user);
            }
            else
                return BadRequest("User doesn't exist.");
        }

        [HttpPost("[action]/")]
        public async Task<IActionResult> ChangePassword(PasswordChange data)
        {
            //var hash_password = SHA1HashStringForUTF8String(data.User_Old_Password);
            var user = await _context.User.Where(u => u.Login == data.User_Login && u.Password.ToUpper() == data.User_Old_Password).SingleOrDefaultAsync(); 
            if (user != null)
            {
                if (user.Password.ToUpper() == data.User_New_Password.ToUpper())
                {
                    //TODO: Change the return for same password
                    return BadRequest("Password cannot be the same");
                }
                user.Password = data.User_New_Password.ToUpper();
                //user.User_password = SHA1HashStringForUTF8String(data.User_New_Password);
                user = await Authenticate(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
                return BadRequest("User doesn't exist.");
        }
        // logowanie 

        // GET: api/Users/5
        [HttpGet("{id}")]
        private async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var new_user = (User)user;
            _context.User.Add(new_user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = new_user.Id }, new_user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        [HttpPost]
        private async Task<User> Authenticate(User user)
        {
            // return null if user not found
            if (user == null)
                return null;

            //// authentication successful so generate jwt token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(SecData.ApiKey);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.UserFirstName.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(360),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //user.Token = tokenHandler.WriteToken(token);
            //await _context.SaveChangesAsync();
            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}