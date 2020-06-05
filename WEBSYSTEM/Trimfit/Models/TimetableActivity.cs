using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class TimetableActivity
    {
        [JsonProperty(PropertyName ="timetable_Activity_Id")]
        public int Timetable_Activity_Id { get; set; }
        [JsonProperty(PropertyName ="employee_Id")]
        public int Employee_Id { get; set; }
        [JsonProperty(PropertyName ="activity_Id")]
        public int Activity_Id { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Day")]
        public string Timetable_Activity_Day { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Starttime")]
        public DateTime Timetable_Activity_Starttime { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Endtime")]
        public DateTime Timetable_Activity_Endtime { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Limit_Places")]
        public int Timetable_Activity_Limit_Places { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Free_Places")]
        public int Timetable_Activity_Free_Places { get; set; }
        [JsonProperty(PropertyName ="room_Id")]
        public int Room_Id { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Repeatable")]
        public int Timetable_Activity_Repeatable { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Status")]
        public int Timetable_Activity_Status { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Reservation_List")]
        public bool Timetable_Activity_Reservation_List { get; set; }
        [JsonProperty(PropertyName ="timetable_Activity_Color")]
        public string Timetable_Activity_Color { get; set; }
        [JsonProperty(PropertyName = "timetable_Id")]
        public int Timetable_Id { get; set; }
        [NotMapped]
        public string Activity_Title { get; set; }
        public Activity Activity { get; set; }

    }
}
