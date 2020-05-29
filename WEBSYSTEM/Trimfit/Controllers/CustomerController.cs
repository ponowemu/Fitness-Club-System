using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trimfit.Models;
using Trimfit.Data;
using Newtonsoft.Json;

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
        public async Task<JsonResult> GetCustomers()
        {
            var customers = await _apiContext.GetRequest("Customers/Details");
            var list = JsonConvert.DeserializeObject(customers.Value.ToString());
            
            
            return new JsonResult(new { data = list });
        }
        [HttpGet]
        public async Task<string> GetVouchers(int customerId)
        {
            var customerVouchers = await _apiContext.GetRequest(String.Format("Customers/Details"));

            return customerVouchers.Value.ToString();
        }
    }
}