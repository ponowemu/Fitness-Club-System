using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class TimetableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("[controller]/[action]/")]
        public async Task<IActionResult> Edit()
        {
            ApiContext _context = new ApiContext();
            List<Timetable> timetables = new List<Timetable>();
            timetables.Clear();

            var timetables_response = await _context.GetRequest("Timetables/");
            var timetables_list = JsonConvert.DeserializeObject<List<Timetable>>(timetables_response.Value.ToString());
            
            foreach (var single_timetable in timetables_list)
            {
                timetables.Add(single_timetable);
            }

            ViewData["Header"] = "Zarządzaj grafikami";
            return View(timetables);
        }
        [HttpGet("[controller]/Edit/{id}")]
        public IActionResult EditTimetable()
        {
            ViewData["Header"] = "Edytuj grafik";
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Add()
        {
            ViewData["Header"] = "Dodaj nowy grafik";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(string timetable_name, string timetable_status)
        {
            ApiContext _context = new ApiContext();
            Timetable timetable = new Timetable();
            string result = "";
            
            try
            {
               
                timetable.Timetable_Name = timetable_name;
                timetable.Timetable_Status = int.Parse(timetable_status);
                timetable.Timetable_Created = DateTime.Now;
                timetable.Timetable_Edited = DateTime.Now;
                timetable.Club_Id = 1;



                JsonResult response = await _context.PostRequest("Timetables/", timetable);
                result = response.Value.ToString();
                // zastanowić się w jaki sposób czytać i przekazywać responsy!!!
            }
            catch(HttpRequestException e)
            {
                result = e.Message;
            }
            
            return new JsonResult(result);
        }
    }
}