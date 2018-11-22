using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TrimFitAPI.Models;

namespace TrimFitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ClassContext _context;

        public ClassController(ClassContext context)
        {
            _context = context;

            if (_context.Classes.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Classes.Add(new Class { Class_description ="Opis", Class_id = 1, Class_name = "Tytuł", Class_active = true });
                _context.Classes.Add(new Class { Class_description = "Opis", Class_id = 2, Class_name = "Tytuł 2", Class_active = true });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Class>> GetAll()
        {
            return _context.Classes.ToList();
        }

        [HttpGet("{id}", Name = "GetClass")]
        public ActionResult<Class> GetById(long id)
        {
            var item = _context.Classes.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

    }


}