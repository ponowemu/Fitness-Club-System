using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class PasswordChange
    {
        public string User_Login { get; set; }
        public string User_Old_Password { get; set; }
        public string User_New_Password { get; set; }
    }
}
