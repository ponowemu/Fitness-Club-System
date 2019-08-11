using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            ApiContext _context = new ApiContext();

            var UserResponse = await _context.PostRequest("Users/Login",user);
            if (UserResponse.Value.ToString() != "400")
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Sean Connery"),
                    new Claim(ClaimTypes.Email, user.User_login)
                };

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