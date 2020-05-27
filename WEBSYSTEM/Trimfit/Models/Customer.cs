using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Customer
    {
        [JsonProperty(PropertyName = "customer_id")]
        public int Customer_Id { get; set; }
        [JsonProperty(PropertyName = "customer_firstname")]
        public string Customer_Firstname { get; set; }
        [JsonProperty(PropertyName = "customer_lastname")]
        public string Customer_Lastname { get; set; }
        [JsonProperty(PropertyName = "customer_birthdate")]
        public DateTime Customer_Birthdate { get; set; }
        [JsonProperty(PropertyName = "customer_gender")]
        public int Customer_Gender { get; set; }
        [JsonProperty(PropertyName = "customer_added")]
        public DateTime Customer_Added { get; set; }
        [JsonProperty(PropertyName = "customer_status")]
        public int Customer_Status { get; set; }
        [JsonProperty(PropertyName = "customer_isconfirmed")]
        public bool Customer_Isconfirmed { get; set; }
        [JsonProperty(PropertyName = "address_id")]
        public int Address_Id { get; set; }
        [JsonProperty(PropertyName = "customer_display_name")]
        public string Customer_Display_Name { get; set; }
        [JsonProperty(PropertyName = "customer_photo_url")]
        public string Customer_Photo_Url { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public int User_Id { get; set; }
        [ForeignKey("Address_Id")]
        public virtual Address Address { get; set; }

    }
}
