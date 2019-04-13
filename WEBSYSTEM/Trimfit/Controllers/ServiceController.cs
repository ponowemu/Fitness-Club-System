using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            List<ServiceView> services_view = new List<ServiceView>();

            ApiContext _context = new ApiContext();

            var employees_response = await _context.GetRequest("Employees/");
            var employees_list = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            var categories_response = await _context.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var services_response = await _context.GetRequest("Services/");
            var services_list = JsonConvert.DeserializeObject<List<Service>>(services_response.Value.ToString());

            foreach (var service in services_list)
            {
                var categories = categories_list.Where(c => service.Category_Id.Contains(c.Category_Id)).ToList();
                var employees = employees_list.Where(e => service.Employee_Id.Contains(e.Employee_Id)).ToList();

                services_view.Add(new ServiceView()
                {
                    Category = categories,
                    Employee = employees,
                    Service_Description = service.Service_Description,
                    Service_Gross_Price = service.Service_Gross_Price,
                    Service_Id = service.Service_Id,
                    Service_Name = service.Service_Name,
                    Service_Net_Price = service.Service_Net_Price,
                    Service_Timelimit_Fri = service.Service_Timelimit_Fri,
                    Service_Timelimit_Mon = service.Service_Timelimit_Mon,
                    Service_Timelimit_Sat = service.Service_Timelimit_Sat,
                    Service_Timelimit_Sun = service.Service_Timelimit_Sun,
                    Service_Timelimit_Thu = service.Service_Timelimit_Thu,
                    Service_Timelimit_Tue = service.Service_Timelimit_Tue,
                    Service_Timelimit_Wed = service.Service_Timelimit_Wed
                });

            }

            ViewData["header"] = "Przeglądaj usługi";
            return View(services_view);
        }
        public IActionResult Manage()
        {
            ViewData["header"] = "Zarządzaj usługami";
            return View();
        }
    }
}
