using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Address
    {
        [JsonProperty(PropertyName ="address_id")]
        public int Address_Id { get; set; }
        [JsonProperty(PropertyName ="address_1")]
        public string Address_1 { get; set; }
        [JsonProperty(PropertyName ="address_2")]
        public string Address_2 { get; set; }
        [JsonProperty(PropertyName ="address_city")]
        public string Address_City { get; set; }
        [JsonProperty(PropertyName ="address_country")]
        public string Address_Country { get; set; }
        [JsonProperty(PropertyName ="address_email")]
        public string Address_Email { get; set; }
        [JsonProperty(PropertyName ="address_postcode")]
        public string Address_Postcode { get; set; }
        [JsonProperty(PropertyName ="address_phone")]
        public string Address_Phone { get; set; }
    }
}
