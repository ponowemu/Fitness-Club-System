using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class RoomClub
    {
        [JsonProperty(PropertyName ="room_club_id")]
        public int Room_Club_Id { get; set; }
        [JsonProperty(PropertyName ="room_id")]
        public int Room_Id { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
    }
}
