using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trimfit.Models;
using Trimfit.Data;
using Newtonsoft.Json;
using System.Dynamic;

namespace Trimfit.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IApiContext _apiContext;

        public DashboardController(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = "Your dashboard";

            var ac = await _apiContext.GetRequest("TimetableActivities/?incoming=true&related=true");
            var res = JsonConvert.DeserializeObject<IEnumerable<TimetableActivity>>(ac.Value.ToString());

            var en = await _apiContext.GetRequest("Queue/");
            var entry = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(en.Value.ToString());

            ViewData["recentActivities"] = res
                .OrderByDescending(a =>a.Timetable_Activity_Starttime)
                .Take(5).ToList();

            ViewData["recentEntries"] = entry
                .OrderByDescending(q => q.created)
                .Take(5).ToList();
            return View();
        }

        public async Task<IEnumerable<dynamic>> GetEntries()
        {
            var en = await _apiContext.GetRequest("Queue/");
            var entry = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(en.Value.ToString());

            var temp = entry.GroupBy(e => e.created.ToString("dd-MM-yyyy")).Select(c => new { Day = c.Key, Summ = c.Count() });

            return temp;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
