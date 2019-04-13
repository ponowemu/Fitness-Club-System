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
        [JsonProperty(PropertyName ="timetable_id")]
        public int Timetable_Id { get; set; }
        [JsonProperty(PropertyName ="timetable_name")]
        public string Timetable_Name { get; set; }
        [JsonProperty(PropertyName ="timetable_status")]
        public int Timetable_Status { get; set; }
        [JsonProperty(PropertyName ="timetable_created")]
        public DateTime Timetable_Created { get; set; }
        [JsonProperty(PropertyName ="timetable_edited")]
        public DateTime Timetable_Edited { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
    }
}
