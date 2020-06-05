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
        private readonly IApiContext _apiContext;
        public ServiceController(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IEnumerable<Club>> GetClubs()
        {
            var res = await _apiContext.GetRequest("Clubs/");
            return JsonConvert.DeserializeObject<IEnumerable<Club>>(res.Value.ToString());
        }
        public async Task<IEnumerable<EmployeeClub>> GetEmployees(int clubId)
        {
            var res = await _apiContext.GetRequest("EmployeeClubs/" + clubId);
            return JsonConvert.DeserializeObject<IEnumerable<EmployeeClub>>(res.Value.ToString());
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var res = await _apiContext.GetRequest("Categories/");
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(res.Value.ToString());
        }
        [HttpGet("[controller]/[action]/{serviceId}")]
        public async Task<IActionResult> Edit(int serviceId)
        {
            var service_response = await _apiContext.GetRequest("Services/" + serviceId + "");
            var service_details = JsonConvert.DeserializeObject<Service>(service_response.Value.ToString());

            var employees_response = await _apiContext.GetRequest("Employees/");
            var employees_list = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            var categories_response = await _apiContext.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var club = await _apiContext.GetRequest("Clubs/");
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

            return View(result);
        }
        [HttpDelete]
        public async Task<JsonResult> Delete(int serviceId)
        {
            var res = await _apiContext.DeleteRequest("Services/" + serviceId);
            return res;
        }
        public async Task<List<ServiceView>> GetServicesList()
        {
            List<ServiceView> services_view = new List<ServiceView>();


            var employees_response = await _apiContext.GetRequest("Employees/");
            var employees_list = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            var categories_response = await _apiContext.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var services_response = await _apiContext.GetRequest("Services/");
            var services_list = JsonConvert.DeserializeObject<List<Service>>(services_response.Value.ToString());

            var club = await _apiContext.GetRequest("Clubs/");
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
                    Service_Duration = service.Service_Duration,
                    Service_Photo_Url = service.Service_Photo_Url
                });

            }

            return services_view;
        }
        [HttpPost]
        public async Task<JsonResult> PostService(List<int> category_id, string service_name, string service_description, decimal service_gross_price, string service_net_price, List<int> service_employees_list, List<int> service_clubs_list, string mon_from, string mon_to, string tue_from, string tue_to, string wen_from, string wen_to, string thu_from, string thu_to, string fri_from, string fri_to, string sat_from, string sat_to, string sun_from, string sun_to, int service_duration)
        {
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
                JsonResult response = await _apiContext.PostRequest("Services/", service);
                result = response.Value.ToString();
                // zastanowić się w jaki sposób czytać i przekazywać responsy!!!
            }
            catch (HttpRequestException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }
        [HttpPut]
        public async Task<JsonResult> PutService(int service_id, List<int> category_id, string service_name, string service_description, decimal service_gross_price, string service_net_price, List<int> service_employees_list, List<int> service_clubs_list, string mon_from, string mon_to, string tue_from, string tue_to, string wen_from, string wen_to, string thu_from, string thu_to, string fri_from, string fri_to, string sat_from, string sat_to, string sun_from, string sun_to, int service_duration)
        {
            string result = "";

            var service = new Service()
            {
                Service_Id = service_id,
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
                JsonResult response = await _apiContext.PutRequest("Services/" + service.Service_Id, service);
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

            var service_response = await _apiContext.GetRequest("Services/" + service_id + "");
            var service_details = JsonConvert.DeserializeObject<Service>(service_response.Value.ToString());

            var employees_response = await _apiContext.GetRequest("Employees/");
            var employees_list = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            var categories_response = await _apiContext.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var club = await _apiContext.GetRequest("Clubs/");
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
        [HttpGet]
        public async Task<JsonResult> CheckAvailability(DateTime date, int service_id)
        {
            // pytanie czy to po tej stronie czy po stronie API 
            // dodać jeszcze sprawdzenie ograniczeń czasowych po stronie usługi

            try
            {
                var response = await _apiContext.GetRequest("Reservations/");
                var reservations = JsonConvert.DeserializeObject<List<Reservation>>(response.Value.ToString());

                var list = reservations.Where(p => p.Service_Id == service_id && p.Reservation_From == date).ToList();

                if (list == null)
                    return new JsonResult(200);
                else
                    return new JsonResult(400);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
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
