using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string errorMsg)
        {
            ViewData["error"] = errorMsg;
            return View();
        }

        private string HashPassword(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            ApiContext _context = new ApiContext();
            var pass = HashPassword(user.User_password);
            user.User_password = pass.ToUpper();

            var UserResponse = await _context.PostRequest("Users/Login",user);
            
            if (UserResponse.Value.ToString() != "400")
            {
                var user_result = JsonConvert.DeserializeObject<User>(UserResponse.Value.ToString());
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.User_login),
                    new Claim(ClaimTypes.Email, user.User_login),
                };
                _context.setToken(user_result.User_Token);
                ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                        scheme: "APIauth",
                        principal: principal);
            }
            else
            {
                return RedirectToAction("Login", new { errorMsg = UserResponse.Value.ToString() } );
            }

            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    scheme: "APIauth");

            return RedirectToAction("Login");
        }

    }
}