using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class EmployeeClub
    {
        [JsonProperty(PropertyName ="employee_club_id")]
        public int Employee_Club_Id { get; set; }
        [JsonProperty(PropertyName ="employee_id")]
        public int Employee_Id { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
    }
}
