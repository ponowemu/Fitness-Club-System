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
           
    }
}
