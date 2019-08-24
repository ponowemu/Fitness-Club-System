using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class UserLogin
    {
        [Column("user_login")]
        public string User_login { get; set; }
        [Column("user_password")]
        public virtual string User_password { get; set; }
    }
}
