using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class ActivityController : Controller
    {
        public async Task<JsonResult> AddAsync(string activity_name, string activity_description, int activity_status, string activity_color, List<int> employee_id, List<int> category_id)
        {
            try
            {
                ApiContext _context = new ApiContext();

                var activity = new Activity()
                {
                    Activity_Color = activity_color,
                    Activity_Description = activity_description,
                    Activity_Name = activity_name,
                    Activity_Status = activity_status,
                    Category_Id = category_id,
                    Employee_Id = employee_id
                };
                JsonResult response = await _context.PostRequest("Activities/", activity);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }

            Response.StatusCode = 200;
            return new JsonResult("Dodano nową aktywność pomyślnie");
        }
        public async Task<JsonResult> DeleteAsync(int id)
        {
            //TODO 
            // Koniecznie dodać podczas usuwania sprawdzenie
            // czy dana aktywność nie jest połączona z jakimś grafikiem. 
            try
            {
                ApiContext _context = new ApiContext();
                JsonResult response = await _context.DeleteRequest("Activities/" + id + "");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
            Response.StatusCode = 200;
            return new JsonResult("Usunięto");
        }
        public async Task<List<Activity>> GetAsync()
        {
            ApiContext _context = new ApiContext();
            List<Activity> activities = new List<Activity>();

            var activities_response = await _context.GetRequest("Activities/");
            var activities_list = JsonConvert.DeserializeObject<List<Activity>>(activities_response.Value.ToString());

            activities = activities_list.ToList();

            return activities;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            ApiContext _context = new ApiContext();
            var categories_response = await _context.GetRequest("Categories/");
            var categories = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            return categories;
        }
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            ApiContext _context = new ApiContext();
            var employees_response = await _context.GetRequest("Employees/");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employees_response.Value.ToString());

            return employees;
        }
        public async Task<Employee> GetEmployeeAsync(int employee_id)
        {
            ApiContext _context = new ApiContext();

            var employee_response = await _context.GetRequest("Employees/" + employee_id + "");
            var employee = JsonConvert.DeserializeObject<Employee>(employee_response.Value.ToString());

            return employee;
        }
        [HttpGet("[controller]/[action]/{activity_id}")]
        public async Task<JsonResult> GetActivityEmployees(int activity_id)
        {
            List<Employee> elist = new List<Employee>();
            ApiContext _context = new ApiContext();
            try
            {
                var activities_response = await _context.GetRequest("Activities/" + activity_id + "/");
                var activities_list = JsonConvert.DeserializeObject<Activity>(activities_response.Value.ToString());
        
                foreach (var item in activities_list.Employee_Id)
                {
                    var el = await this.GetEmployeeAsync(item);
                    elist.Add(el);
                }
                return new JsonResult(elist);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
           
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }
        public async Task<IActionResult> List()
        {
            var activities = await this.GetAsync();
            ViewData["categories"] = await this.GetCategoriesAsync();
            ViewData["employees"] = await this.GetEmployeesAsync();
            List<ActivityView> activities_view = new List<ActivityView>();
            foreach (var el in activities)
            {
                var employees_list = new List<Employee>();

                if (el.Employee_Id != null)
                {
                    foreach (var employee in el.Employee_Id)
                    {
                        employees_list.Add(await this.GetEmployeeAsync(employee));
                    }
                }

                activities_view.Add(new ActivityView()
                {
                    Activity_Color = el.Activity_Color,
                    Activity_Description = el.Activity_Description,
                    Activity_Id = el.Activity_Id,
                    Activity_Name = el.Activity_Name,
                    Activity_Status = el.Activity_Status,
                    Employee = employees_list,
                    Category_Id = el.Category_Id
                });
            }
            ViewData["Header"] = "Lista zajęć";
            return View(activities_view);
        }
    }
}