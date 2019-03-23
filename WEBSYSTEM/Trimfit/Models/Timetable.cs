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
        public int Timetable_Id { get; set; }
        public string Timetable_Name { get; set; }
        public int Timetable_Status { get; set; }
        public DateTime Timetable_Created { get; set; }
        public DateTime Timetable_Edited { get; set; }
        public int Club_Id { get; set; }
    }
}
