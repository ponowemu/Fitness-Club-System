using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    [Authorize]
    public class TimetableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("[controller]/[action]/")]
        public async Task<IActionResult> Edit()
        {
            // Lista wszystkich utworzonych grafików;

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
        public async Task<IActionResult> EditTimetable()
        {
            ViewData["Header"] = "Edytuj grafik";

            var activities = new ActivityController();
            ViewData["activities"] = await activities.GetAsync();

            var rooms = new RoomController();
            ViewData["rooms"] = await rooms.GetAsync();

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
            catch (HttpRequestException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }
        public async Task<JsonResult> GetActivityAsync(int id)
        {
            ApiContext _context = new ApiContext();
            try
            {
                var result = await _context.GetRequest("TimetableActivities/" + id + "");

                Response.StatusCode = 200;
                return new JsonResult(result.Value);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
        }
        public async Task<JsonResult> GetTimetableActivityAsync(int timetable_id)
        {
            ApiContext _context = new ApiContext();
            try
            {
                var result = await _context.GetRequest("TimetableActivities/?timetable=20");
                var list = JsonConvert.DeserializeObject<List<TimetableActivity>>(result.Value.ToString()).Where(l => l.Timetable_Id == timetable_id).Take(100);
                
                foreach(var el in list)
                {
                    var ac = await _context.GetRequest("Activities/" + el.Activity_Id + "");
                    var activity = JsonConvert.DeserializeObject<Activity>(ac.Value.ToString());

                    el.Activity_Title = activity.Activity_Name;
                }
                

                Response.StatusCode = 200;
                return new JsonResult(list);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
        }
        public async Task<JsonResult> AddActivityAsync([FromBody] TimetableActivity timetable_activity)
        {
            ApiContext _context = new ApiContext();

            try
            {
                var result = await _context.PostRequest("TimetableActivities/", timetable_activity);
                Response.StatusCode = 200;
                return new JsonResult(result.Value);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
        }
        [HttpPut]
        public async Task<JsonResult> EditActivityTimeModelAsync([FromBody] TimetableActivity ta)
        {
           
            try
            {
                ApiContext _context = new ApiContext();
                var response = await this.GetActivityAsync(ta.Timetable_Activity_Id);
                var element = JsonConvert.DeserializeObject<TimetableActivity>(response.Value.ToString());
                element.Timetable_Activity_Color = ta.Timetable_Activity_Color;
                element.Room_Id = ta.Room_Id;
                element.Employee_Id = ta.Employee_Id;
                element.Timetable_Activity_Free_Places = ta.Timetable_Activity_Free_Places;
                element.Timetable_Activity_Limit_Places = ta.Timetable_Activity_Limit_Places;
                element.Timetable_Activity_Reservation_List = ta.Timetable_Activity_Reservation_List;

                var result = await _context.PutRequest("TimetableActivities/" + ta.Timetable_Activity_Id + "", element);

                Response.StatusCode = 200;
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
        }
        [HttpPut]
        public async Task<JsonResult> EditActivityTimeAsync(int id, string starttime, string endtime, string day)
        {
            ApiContext _context = new ApiContext();

            try
            {

                var dateStartTimeOffset = DateTimeOffset.Parse(starttime, null);
                var dateEndTimeOffset = DateTimeOffset.Parse(endtime, null);

                var response = await this.GetActivityAsync(id);
                var element = JsonConvert.DeserializeObject<TimetableActivity>(response.Value.ToString());
                element.Timetable_Activity_Starttime = dateStartTimeOffset.DateTime;
                element.Timetable_Activity_Endtime = dateEndTimeOffset.DateTime;
                element.Timetable_Activity_Day = day;

                var result = await _context.PutRequest("TimetableActivities/" + id + "", element);

                Response.StatusCode = 200;
                return new JsonResult(result);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteTimetableActivity(int taId)
        {
            try
            {
                ApiContext _context = new ApiContext();
                var response = await _context.DeleteRequest("TimetableActivities/"+taId+"");
                Response.StatusCode = 200;

                return new JsonResult(response);
            }
            catch
            {
                Response.StatusCode = 400;
                return new JsonResult("Something went wrong");
            }
        }
    }
}