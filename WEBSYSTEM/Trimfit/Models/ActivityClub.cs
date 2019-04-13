using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class ActivityClub
    {
        [JsonProperty(PropertyName ="activity_club_id")]
        public int Activity_Club_Id { get; set; }
        [JsonProperty(PropertyName ="activity_id")]
        public int Activity_Id { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
    }
}
