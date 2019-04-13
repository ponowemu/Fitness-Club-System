using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Payment
    {
        [JsonProperty(PropertyName ="payment_id")]
        public int Payment_Id { get; set; }
        [JsonProperty(PropertyName ="customer_id")]
        public int Customer_Id { get; set; }
        [JsonProperty(PropertyName ="payment_type_id")]
        public int Payment_Type_Id {get;set;}
        [JsonProperty(PropertyName ="payment_status")]
        public int Payment_Status { get; set; }
        [JsonProperty(PropertyName ="payment_time")]
        public DateTime Payment_Time { get; set; }
    }
}
