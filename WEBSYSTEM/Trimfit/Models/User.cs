using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class User
    {
        [JsonProperty(PropertyName ="user_id")]
        public int User_id { get; set; }
        [JsonProperty(PropertyName ="user_login")]
        public string User_login { get; set; }
        [JsonProperty(PropertyName ="user_password")]
        public string User_password { get; set; }
        [JsonProperty(PropertyName ="user_type")]
        public int User_Type { get; set; }
        [JsonProperty(PropertyName ="user_status")]
        public int User_Status { get; set; }
        [JsonProperty(PropertyName ="user_photo_url")]
        public string User_Photo_Url { get; set; }
        [JsonProperty(PropertyName ="user_firstname")]
        public string User_FirstName { get; set; }
        [JsonProperty(PropertyName ="user_lastname")]
        public string User_LastName { get; set; }
        [JsonProperty(PropertyName = "user_token")]
        public string User_Token { get; set; }

    }
}
