using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Employee
    {
        public int Employee_Id { get; set; }
        public string Employee_Firstname { get; set; }
        public string Employee_Lastname { get; set; }
        public DateTime Employee_Birthdate { get; set; }
        public string Employee_Address1 { get; set; }
        public string Employee_Address2 { get; set; }
        public string Employee_Postcode { get; set; }
        public string Employee_Email { get; set; }
        public string Employee_Phone { get; set; }
        public int Employee_Gender { get; set; }
        public DateTime Employee_Added { get; set; }
        public int Employee_Position_Id { get; set; }
        public int Employee_Status { get; set; }
    }
}
