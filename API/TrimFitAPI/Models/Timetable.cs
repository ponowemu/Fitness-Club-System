using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("timetable")]
    public class Timetable
    {
        [Key]
        [Column("timetable_id")]
        public int Timetable_Id { get; set; }
        [Column("timetable_name")]
        public int Timetable_Name { get; set; }
        [Column("timetable_status")]
        public int Timetable_Status { get; set; }
        [Column("timetable_created")]
        public DateTime Timetable_Created { get; set; }
        [Column("timetable_edited")]
        public DateTime Timetable_Edited { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
    }
}
