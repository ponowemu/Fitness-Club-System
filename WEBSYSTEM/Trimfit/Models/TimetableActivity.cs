using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class TimetableActivity
    {
        public int Timetable_Activity_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Activity_Id { get; set; }
        public string Timetable_Activity_Day { get; set; }
        public DateTime Timetable_Activity_Starttime { get; set; }
        public DateTime Timetable_Activity_Endtime { get; set; }
        public int Timetable_Activity_Limit_Places { get; set; }
        public int Timetable_Activity_Free_Places { get; set; }
        public int Room_Id { get; set; }
        public int Timetable_Activity_Repeatable { get; set; }
        public int Timetable_Activity_Status { get; set; }
        public bool Timetable_Activity_Reservation_List { get; set; }

    }
}
