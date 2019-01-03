using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Timetable
    {
        [Key]
        [Column("timetable_id")]
        public int Timetable_Id { get; set; }
        [Column("timetable_employee_id")]
        public int Timetable_Employee_Id { get; set; }
        [Column("timetable_activity_id")]
        public int Timetable_Activity_Id { get; set; }
        [Column("timetable_day")]
        public string Timetable_Day { get; set; }
        [Column("timetable_starttime")]
        public DateTime Timetable_Starttime { get; set; }
        [Column("timetable_endtime")]
        public DateTime Timetable_Endtime { get; set; }
        [Column("timetable_places")]
        public int Timetable_Limit_Places { get; set; }
        [Column("timetable_free_places")]
        public int Timetable_Free_Places { get; set; }
        [Column("timetable_room_id")]
        public int Timetable_Room_Id { get; set; }
        [Column("timetable_repeatable")]
        public int Timetable_Repeatable { get; set; }
        [Column("timetable_status")]
        public int Timetable_Status { get; set; }
        [Column("timetable_reservation_list")]
        public bool Timetable_Reservation_List { get; set; }

    }
}
