using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trimfit.Controllers
{
    public class TimetableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("[controller]/[action]/")]
        public IActionResult Edit()
        {
            ViewData["Header"] = "Zarządzaj grafikami";
            return View();
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
            return View();
        }
    }
}