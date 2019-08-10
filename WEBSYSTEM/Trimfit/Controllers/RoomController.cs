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
    public class RoomController : Controller
    {
       
        public async Task<List<Room>> GetAsync()
        {
            ApiContext _context = new ApiContext();
            try
            {
                var rooms_response = await _context.GetRequest("Rooms/");
                var rooms = JsonConvert.DeserializeObject<List<Room>>(rooms_response.Value.ToString());
                return rooms;
            }
            catch(Exception ex)
            {
                return new List<Room>();
            }
        }
    }
}