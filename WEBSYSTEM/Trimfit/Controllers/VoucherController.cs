using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;
using Trimfit.Models.ModelView;

namespace Trimfit.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IApiContext _apiContext;
        public VoucherController(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public IActionResult List()
        {
            ViewData["Header"] = "Lista dostępnych karnetów";

            return View();
        }

        public async Task<JsonResult> AddNewVoucher(Voucher voucher)
        {
            var res = await _apiContext.PostRequest("Vouchers/", voucher);
            return res;
        }
        public async Task<IActionResult> Manage()
        {
            ViewData["Header"] = "Zarządzaj karnetami";

            var list = await _apiContext.GetRequest("Vouchers/");
            var voucherList = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(list.Value.ToString());

            var activitiesList = new List<Activity>();


            

            return View(voucherList);
        }
        public async Task<IActionResult> Add()
        {
            var result = await _apiContext.GetRequest("VoucherTypes/");
            var activities = await _apiContext.GetRequest("Activities/");

            ViewData["voucherTypes"] = JsonConvert.DeserializeObject<IEnumerable<VoucherType>>(result.Value.ToString());
            ViewData["activities"] = JsonConvert.DeserializeObject<IEnumerable<Activity>>(activities.Value.ToString());

            return View();
        }
    }
}