using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Timetable
    {
        [Key]
        [JsonProperty("timetable_Id")]
        public int Timetable_Id { get; set; }
        [JsonProperty("timetable_Name")]
        public string Timetable_Name { get; set; }
        [JsonProperty("timetable_Status")]
        public int? Timetable_Status { get; set; }
        [JsonProperty("timetable_Created")]
        public DateTime Timetable_Created { get; set; }
        [JsonProperty("timetable_Edited")]
        public DateTime Timetable_Edited { get; set; }
        [JsonProperty("club_Id")]
        public int? Club_Id { get; set; }
    }
}
