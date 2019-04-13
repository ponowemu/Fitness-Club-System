using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class PositionClub
    {
        [JsonProperty(PropertyName ="position_club_id")]
        public int Position_Club_Id { get; set; }
        [JsonProperty(PropertyName ="position_id")]
        public int Position_Id { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
    }
}
