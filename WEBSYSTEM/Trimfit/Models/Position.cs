using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Position
    {
        [JsonProperty(PropertyName ="position_id")]
        public int Position_Id { get; set; }
        [JsonProperty(PropertyName ="position_name")]
        public string Position_Name { get; set; }
        [JsonProperty(PropertyName ="position_status")]
        public bool Position_Status { get; set; }
    }
}
