using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trimfit.Models;
using Trimfit.Data;

namespace Trimfit.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IApiContext _apiContext;
        public CustomerController(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            ViewData["Header"] = "Lista klientów";
            return View();
        }
        public async Task<JsonResult> AddAsync(Customer customer)
        {
            var res = await _apiContext.PostRequest("Customers/", customer);
            return res;
        }
    }
}