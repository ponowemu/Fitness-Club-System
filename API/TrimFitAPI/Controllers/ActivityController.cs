using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TrimFitAPI.Models;

namespace TrimFitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ApiContext _context;

        public ActivityController(ApiContext context)
        {
            _context = context;

            /*
            if (_context.Classes.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Classes.Add(new Class { Class_description ="Opis", Class_name = "Tytuł", Class_active = true });
                _context.Classes.Add(new Class { Class_description = "Opis", Class_name = "Tytuł 2", Class_active = true });
                _context.Classes.Add(new Class { Class_description = "Opis", Class_name = "Tytuł 2", Class_active = true });
                _context.SaveChanges();
            }
            */
        }

        [HttpGet]
        public ActionResult<List<Activity>> GetAll()
        {
            return _context.Activity.ToList();
        }

        [HttpGet("{id}", Name = "GetClass")]
        public ActionResult<Activity> GetById(long id)
        {
        
            var item = _context.Activity.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpPost]
        public void AddClass()
        {
            _context.Activity.Add(new Activity { Activity_Name = "aa", Activity_Status = 1});
            _context.SaveChanges();
        }

    }


}