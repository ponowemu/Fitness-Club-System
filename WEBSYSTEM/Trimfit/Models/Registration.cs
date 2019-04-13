using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Registration
    {
        [JsonProperty(PropertyName ="registration_id")]
        public int Registration_Id { get; set; }
        [JsonProperty(PropertyName ="timetable_id")]
        public int Timetable_Id { get; set; }
        [JsonProperty(PropertyName ="customer_id")]
        public int Customer_Id { get; set; }
        [JsonProperty(PropertyName ="registration_created")]
        public DateTime Registartion_Created { get; set; }
        [JsonProperty(PropertyName ="payment_id")]
        public int Payment_Id { get; set; }
        [JsonProperty(PropertyName ="registration_type")]
        public int Registration_Type { get; set; }

    }
}
