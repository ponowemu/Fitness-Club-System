using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            ViewData["header"] = "Przeglądaj usługi";
            return View();
        }
        public IActionResult Manage()
        {
            ViewData["header"] = "Zarządzaj usługami";
            return View();
        }
    }
}
