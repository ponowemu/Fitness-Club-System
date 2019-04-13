using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Room
    {
        [JsonProperty(PropertyName ="room_id")]
        public int Room_Id { get; set; }
        [JsonProperty(PropertyName ="room_name")]
        public string Room_Name { get; set; }
        [JsonProperty(PropertyName ="room_description")]
        public string Room_Descripition { get; set; }
    }
}
