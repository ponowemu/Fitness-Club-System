using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class CustomerClub
    {
        [JsonProperty(PropertyName ="customer_club_id")]
        public int Customer_Club_Id { get; set; }
        [JsonProperty(PropertyName ="customer_id")]
        public int Customer_Id { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public int Club_Id { get; set; }
    }
}
