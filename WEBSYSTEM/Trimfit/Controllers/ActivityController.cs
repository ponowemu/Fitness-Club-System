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
        public async Task<List<Activity>> GetAsync()
        {
            ApiContext _context = new ApiContext();
            List<Activity> activities = new List<Activity>();

            var activities_response = await _context.GetRequest("Activities/");
            var activities_list = JsonConvert.DeserializeObject<List<Activity>>(activities_response.Value.ToString());
            
            activities = activities_list.ToList();

            return activities;
        }
        public async Task<Employee> GetEmployeeAsync(int employee_id)
        {
            ApiContext _context = new ApiContext();

            var employee_response = await _context.GetRequest("Employees/" + employee_id + "");
            var employee = JsonConvert.DeserializeObject<Employee>(employee_response.Value.ToString());

            return employee;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        public async Task<IActionResult> List()
        {
            var activities = await this.GetAsync();
            List<ActivityView> activities_view = new List<ActivityView>();
            foreach (var el in activities)
            {
                var employees_list = new List<Employee>();

                if(el.Employee_Id != null)
                {
                    foreach (var employee in el.Employee_Id)
                    {
                        employees_list.Add(await this.GetEmployeeAsync(employee));
                    }
                }
                

                activities_view.Add(new ActivityView() {
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