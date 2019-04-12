using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int User_id { get; set; }
        [Column("user_login")]
        public string User_login { get; set; }
        [Column("user_password")]
        public string User_password { get; set; }
        [Column("user_type")]
        public int User_Type { get; set; }
        [Column("customer_id")]
        public int Customer_Id { get; set; }
        [Column("user_status")]
        public int User_Status { get; set; }
        [Column("user_photo_url")]
        public string User_Photo_Url { get; set; }
        [Column("user_firstname")]
        public string User_FirstName { get; set; }
        [Column("user_lastname")]
        public string User_LastName { get; set; }

    }
}
