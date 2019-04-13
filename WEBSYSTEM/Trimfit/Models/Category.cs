using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Category
    {
        [JsonProperty(PropertyName ="category_id")]
        public int Category_id { get; set; }
        [JsonProperty(PropertyName ="category_name")]
        public string Category_name { get; set; }
    }
}
