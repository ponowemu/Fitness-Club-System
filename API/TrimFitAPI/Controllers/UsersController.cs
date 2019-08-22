using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrimFitAPI.Models;

namespace TrimFitAPI.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }


        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

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
        //logowanie
        [AllowAnonymous]
        [HttpPost("[action]/")]
        public async Task<IActionResult> Login(UserLogin temp_user)
        {
            var hash_password = SHA1HashStringForUTF8String(temp_user.User_password);
            var user = await _context.User.Where(u => u.User_login == temp_user.User_login && u.User_password.ToLower() == hash_password.ToString()).SingleOrDefaultAsync();
            user = await Authenticate(user);
            if (user != null)
            {
                return Ok(user);
            }
            else
                return BadRequest("User doesn't exist.");
        }

        [AllowAnonymous]
        [HttpPost("[action]/")]
        public async Task<IActionResult> ChangePassword(PasswordChange data)
        {
            var hash_password = SHA1HashStringForUTF8String(data.User_Old_Password);
            var user = await _context.User.Where(u => u.User_login == data.User_Login.ToLower() && u.User_password.ToLower() == hash_password.ToString()).SingleOrDefaultAsync();       
            if (user != null)
            {
                user.User_password = SHA1HashStringForUTF8String(data.User_New_Password);
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
        private async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_id)
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
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.User_id }, user);
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

        private async Task<User> Authenticate(User user)
        {
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecData.ApiKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User_FirstName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(360),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.User_Token = tokenHandler.WriteToken(token);
            await _context.SaveChangesAsync();
            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.User_id == id);
        }
    }
}