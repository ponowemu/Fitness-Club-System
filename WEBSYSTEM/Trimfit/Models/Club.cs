using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Club
    {
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
        [JsonProperty(PropertyName ="club_name")]
        public string Club_Name { get; set; }
        [JsonProperty(PropertyName ="club_status")]
        public int Club_Status { get; set; }
        [JsonProperty(PropertyName ="address_id")]
        public int Address_Id { get; set; }
    }
}
