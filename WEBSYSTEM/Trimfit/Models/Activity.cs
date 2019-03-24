using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
 
    public class Activity
    {
        [JsonProperty(PropertyName = "activity_id")]
        public int Activity_Id { get; set; }
        [JsonProperty(PropertyName = "activity_name")]
        public string Activity_Name { get; set; }
        [JsonProperty(PropertyName = "activity_description")]
        public string Activity_Description { get; set; }
        [JsonProperty(PropertyName = "activity_status")]
        public int Activity_Status { get; set; }
    }
}
