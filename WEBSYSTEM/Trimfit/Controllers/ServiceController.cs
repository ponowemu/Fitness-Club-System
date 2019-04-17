using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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
        public async Task<List<ServiceView>> GetServicesList()
        {
            List<ServiceView> services_view = new List<ServiceView>();

            ApiContext _context = new ApiContext();

            var employees_response = await _context.GetRequest("Employees/");
            var employees_list = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            var categories_response = await _context.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var services_response = await _context.GetRequest("Services/");
            var services_list = JsonConvert.DeserializeObject<List<Service>>(services_response.Value.ToString());

            var club = await _context.GetRequest("Clubs/");
            var clubs_list = JsonConvert.DeserializeObject<List<Club>>(club.Value.ToString());

            foreach (var service in services_list)
            {
                var categories = categories_list.Where(c => service.Category_Id.Contains(c.Category_Id)).ToList();
                var employees = employees_list.Where(e => service.Employee_Id.Contains(e.Employee_Id)).ToList();
                var clubs = clubs_list.Where(c => service.Club_Id.Contains(c.Club_Id)).ToList();

                services_view.Add(new ServiceView()
                {
                    Club = clubs,
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
                    Service_Timelimit_Wed = service.Service_Timelimit_Wed,
                    Service_Duration = service.Service_Duration
                });

            }

            return services_view;
        }
        [HttpPost]
        public async Task<JsonResult> PostService(List<int> category_id, string service_name, string service_description, decimal service_gross_price, string service_net_price, List<int> service_employees_list, List<int> service_clubs_list, string mon_from, string mon_to, string tue_from, string tue_to, string wen_from, string wen_to, string thu_from, string thu_to, string fri_from, string fri_to, string sat_from, string sat_to, string sun_from, string sun_to, int service_duration)
        {
            ApiContext _context = new ApiContext();
            string result = "";

            var service = new Service()
            {
                Club_Id = service_clubs_list,
                Employee_Id = service_employees_list,
                Service_Description = service_description,
                Service_Gross_Price = service_gross_price,
                Service_Net_Price = Math.Round(Convert.ToDecimal(service_net_price.Replace(".", ",")), 2),
                Service_Name = service_name,
                Category_Id = category_id,
                Service_Timelimit_Mon = new List<TimeSpan>() { TimeSpan.Parse(mon_from + ":00"), TimeSpan.Parse(mon_to + ":00") },
                Service_Timelimit_Tue = new List<TimeSpan>() { TimeSpan.Parse(tue_from + ":00"), TimeSpan.Parse(tue_to + ":00") },
                Service_Timelimit_Wed = new List<TimeSpan>() { TimeSpan.Parse(wen_from + ":00"), TimeSpan.Parse(wen_to + ":00") },
                Service_Timelimit_Thu = new List<TimeSpan>() { TimeSpan.Parse(thu_from + ":00"), TimeSpan.Parse(thu_to + ":00") },
                Service_Timelimit_Fri = new List<TimeSpan>() { TimeSpan.Parse(fri_from + ":00"), TimeSpan.Parse(fri_to + ":00") },
                Service_Timelimit_Sat = new List<TimeSpan>() { TimeSpan.Parse(sat_from + ":00"), TimeSpan.Parse(sat_to + ":00") },
                Service_Timelimit_Sun = new List<TimeSpan>() { TimeSpan.Parse(sun_from + ":00"), TimeSpan.Parse(sun_to + ":00") },
                Service_Duration = service_duration
            };

            try
            {
                JsonResult response = await _context.PostRequest("Services/", service);
                result = response.Value.ToString();
                // zastanowić się w jaki sposób czytać i przekazywać responsy!!!
            }
            catch (HttpRequestException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<ServiceView> GetDetails(int service_id)
        {
            ApiContext _context = new ApiContext();

            var service_response = await _context.GetRequest("Services/" + service_id + "");
            var service_details = JsonConvert.DeserializeObject<Service>(service_response.Value.ToString());

            var employees_response = await _context.GetRequest("Employees/");
            var employees_list = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            var categories_response = await _context.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var club = await _context.GetRequest("Clubs/");
            var clubs_list = JsonConvert.DeserializeObject<List<Club>>(club.Value.ToString());

            var categories = categories_list.Where(c => service_details.Category_Id.Contains(c.Category_Id)).ToList();
            var employees = employees_list.Where(e => service_details.Employee_Id.Contains(e.Employee_Id)).ToList();
            var clubs = clubs_list.Where(c => service_details.Club_Id.Contains(c.Club_Id)).ToList();

            var result = new ServiceView()
            {
                Club = clubs,
                Category = categories,
                Employee = employees,
                Service_Description = service_details.Service_Description,
                Service_Gross_Price = service_details.Service_Gross_Price,
                Service_Id = service_details.Service_Id,
                Service_Name = service_details.Service_Name,
                Service_Net_Price = service_details.Service_Net_Price,
                Service_Timelimit_Fri = service_details.Service_Timelimit_Fri,
                Service_Timelimit_Mon = service_details.Service_Timelimit_Mon,
                Service_Timelimit_Sat = service_details.Service_Timelimit_Sat,
                Service_Timelimit_Sun = service_details.Service_Timelimit_Sun,
                Service_Timelimit_Thu = service_details.Service_Timelimit_Thu,
                Service_Timelimit_Tue = service_details.Service_Timelimit_Tue,
                Service_Timelimit_Wed = service_details.Service_Timelimit_Wed,
                Service_Duration = service_details.Service_Duration
            };

            return result;
        }
        public async Task<IActionResult> List()
        {

            ViewData["header"] = "Przeglądaj usługi";
            return View(await this.GetServicesList());
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await this.GetDetails(id);
            ViewData["Header"] = result.Service_Name;
            return View(result);
        }
        public async Task<IActionResult> Manage()
        {
            var services_list = await this.GetServicesList();

            ViewData["header"] = "Zarządzaj usługami";
            return View(services_list);
        }
        public IActionResult Add()
        {
            ViewData["header"] = "Dodaj nową usługę";
            return View();
        }
    }
}
