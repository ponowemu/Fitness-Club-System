using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("timetable_activity")]
    public class TimetableActivity : IDatabaseEntity
    {
        [Key]
        [Column("timetable_activity_id")]
        public int Id { get; set; }   

        [Column("employee_id")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Column("activity_id")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        [Column("timetable_activity_day")]
        public string Day { get; set; }

        [Column("timetable_activity_starttime")]
        public DateTime Starttime { get; set; }

        [Column("timetable_activity_endtime")]
        public DateTime Endtime { get; set; }

        [Column("timetable_activity_limit_places")]
        public int LimitPlaces { get; set; }

        [Column("timetable_activity_free_places")]
        public int FreePlaces { get; set; }

        [Column("room_id")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Column("timetable_activity_repeatable")]
        public int Repeatable { get; set; }

        [Column("timetable_activity_status")]
        public int Status { get; set; }

        [Column("timetable_activity_reservation_list")]
        public bool ReservationList { get; set; }

        [Column("timetable_activity_color")]
        public string Color { get; set; }

        [Column("timetable_id")]
        public int TimetableId { get; set; }
        public Timetable Timetable { get; set; }

    }
}
