using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Employee
    {
        [JsonProperty(PropertyName ="employee_id")]
        public int Employee_Id { get; set; }
        [JsonProperty(PropertyName ="employee_firstname")]
        public string Employee_Firstname { get; set; }
        [JsonProperty(PropertyName ="employee_lastname")]
        public string Employee_Lastname { get; set; }
        [JsonProperty(PropertyName ="employee_birthdate")]
        public DateTime Employee_Birthdate { get; set; }
        [JsonProperty(PropertyName ="employee_gender")]
        public int Employee_Gender { get; set; }
        [JsonProperty(PropertyName ="employee_added")]
        public DateTime Employee_Added { get; set; }
        [JsonProperty(PropertyName ="employee_position_id")]
        public List<int> Employee_Position_Id { get; set; }
        [JsonProperty(PropertyName ="employee_status")]
        public int Employee_Status { get; set; }
        [JsonProperty(PropertyName ="address_id")]
        public int Address_Id { get; set; }
        [JsonProperty(PropertyName ="employee_display_name")]
        public string Employee_Display_Name { get; set; }
        [JsonProperty(PropertyName ="employee_photo_url")]
        public string Employee_Photo_Url { get; set; }
    }
}
