using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trimfit.Models;
using Trimfit.Data;
using Newtonsoft.Json;
using Trimfit.Models.ModelView;

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
        [HttpDelete]
        public async Task<JsonResult> DeleteAsync(int customerId)
        {
            var res = await _apiContext.DeleteRequest("Customers/" + customerId);
            if(res.Value.ToString() != "200")
            {
                Response.StatusCode = 404;
                return null;
            }
            return res;
        }

        [HttpGet("[controller]/[action]/{customerId}")]
        public async Task<IActionResult> Edit(int customerId)
        {
            var res = await _apiContext.GetRequest("Customers/" + customerId);
            var customer = JsonConvert.DeserializeObject<Customer>(res.Value.ToString());

            var list = await _apiContext.GetRequest("Vouchers/");
            var voucherList = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(list.Value.ToString());

            ViewData["vouchers"] = voucherList;
            ViewData["zapisyKlienta"] = 0;
            ViewData["zakupioneUslugi"] = 0;
            ViewData["zakupioneProdukty"] = 0;

            return View(customer);
        }
        public async Task<JsonResult> AddAsync(Customer customer)
        {
            var res = await _apiContext.PostRequest("Customers/", customer);
            // POWIĄZANIE Z KLUBEM - póki co tymczasowo ustawia

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
        [HttpPut]
        public async Task<JsonResult> Update(Customer customer)
        {
            var addedStatus = await _apiContext.PutRequest("Customers/" + customer.Customer_Id, customer);
            if (addedStatus.Value == "400")
            {
                Response.StatusCode = 404;
                return new JsonResult("ERR");
            }
            else
            {
                Response.StatusCode = 200;
                return addedStatus;
            }
        }
    }
}