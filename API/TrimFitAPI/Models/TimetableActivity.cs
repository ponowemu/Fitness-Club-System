using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("timetable_activity")]
    public class TimetableActivity
    {
        [Key]
        [Column("timetable_activity_id")]
        public int Timetable_Activity_Id { get; set; }   

        [Column("employee_id")]
        public int Employee_Id { get; set; }
        public Employee Employee { get; set; }

        [Column("activity_id")]
        public int Activity_Id { get; set; }
        public Activity Activity { get; set; }

        [Column("timetable_activity_day")]
        public string Timetable_Activity_Day { get; set; }
        [Column("timetable_activity_starttime")]
        public DateTime Timetable_Activity_Starttime { get; set; }
        [Column("timetable_activity_endtime")]
        public DateTime Timetable_Activity_Endtime { get; set; }
        [Column("timetable_activity_limit_places")]
        public int Timetable_Activity_Limit_Places { get; set; }
        [Column("timetable_activity_free_places")]
        public int Timetable_Activity_Free_Places { get; set; }

        [Column("room_id")]
        public int Room_Id { get; set; }
        public Room Room { get; set; }

        [Column("timetable_activity_repeatable")]
        public int Timetable_Activity_Repeatable { get; set; }
        [Column("timetable_activity_status")]
        public int Timetable_Activity_Status { get; set; }
        [Column("timetable_activity_reservation_list")]
        public bool Timetable_Activity_Reservation_List { get; set; }
        [Column("timetable_activity_color")]
        public string Timetable_Activity_Color { get; set; }
        [Column("timetable_id")]

        public int Timetable_Id { get; set; }
        public Timetable Timetable { get; set; }

    }
}
