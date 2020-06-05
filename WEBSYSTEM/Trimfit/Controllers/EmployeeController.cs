using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IApiContext _apiContext;
        private readonly IHostingEnvironment _env;
        public EmployeeController(IApiContext context, IHostingEnvironment env)
        {
            _apiContext = context;
            _env = env;
        }
        public async Task<JsonResult> Add(Employee employee)
        {
            employee.Employee_Added = DateTime.Now;
            employee.Clubs = new List<EmployeeClub>() { new EmployeeClub{
                Club_Id = 1
            }};
            var addedStatus = await _apiContext.PostRequest("Employees", employee);
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
        [HttpPut]
        public async Task<JsonResult> Update(Employee employee)
        {
            var addedStatus = await _apiContext.PutRequest("Employees/" + employee.Employee_Id, employee);
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
        public async Task<IList<string>> UploadFile(IList<IFormFile> formFiles)
        {
            var filePaths = new List<string>();

            foreach (var formFile in formFiles)
            {
                if (formFile.Length > 0)
                {
                    var ext = formFile.FileName.Split('.');
                    var newName = String.Join('.', Guid.NewGuid(), ext[1]);
                    // full path to file in temp location
                    var filePath = Path.Combine(
                        _env.WebRootPath,
                        "upload",
                        newName);
                    filePaths.Add(newName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return filePaths;
        }
        public IActionResult Manage()
        {
            return View();
        }
        [HttpGet("[controller]/[action]/{employeeId}")]
        public async Task<IActionResult> Profile(int employeeId)
        {
            var employee = await _apiContext.GetRequest("Employees/" + employeeId);
            var ep = JsonConvert.DeserializeObject<Employee>(employee.Value.ToString());


            return View(ep);
        }
        public async Task<JsonResult> GetEmployees()
        {
            int clubId = 1;
            var employees = await _apiContext.GetRequest("EmployeeClubs/" + clubId + "");
            var list = JsonConvert.DeserializeObject<IEnumerable<EmployeeClub>>(employees.Value.ToString());

            for (int i = 0; i < list.Count(); i++)
            {
                var em = list.ElementAt(i);
                var det = await _apiContext.GetRequest("Employees/" + em.Employee_Id + "/Activities");
                if (!String.IsNullOrEmpty(det.Value.ToString()))
                {
                    var ac = JsonConvert.DeserializeObject<IEnumerable<Activity>>(det.Value.ToString());
                    em.Employee.Activities = new List<Activity>(ac);
                }
            }

            return new JsonResult(new { data = list });
        }
    }
}