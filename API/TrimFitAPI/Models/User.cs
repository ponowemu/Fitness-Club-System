using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("user")]
    public class User : UserLogin
    {
        [Key]
        [Column("user_id")]
        public int User_id { get; set; }
        [Column("user_token")]
        public string User_Token { get; set; }
        [Column("user_type")]
        public int User_Type { get; set; }
        [Column("user_status")]
        public int User_Status { get; set; }
        [Column("user_photo_url")]
        public string User_Photo_Url { get; set; }
        [Column("user_firstname")]
        public string User_FirstName { get; set; }
        [Column("user_lastname")]
        public string User_LastName { get; set; }
        [JsonIgnore]
        public override string User_password { get => base.User_password; set => base.User_password = value; }

    }
}
