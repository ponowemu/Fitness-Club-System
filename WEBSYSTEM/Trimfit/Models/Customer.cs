using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Customer
    {

        public int Customer_Id {get;set;}
        public string Customer_Firstname { get; set; }
        public string Customer_Lastname { get; set; }
        public DateTime Customer_Birthdate { get; set; }
        public string Customer_Address1 { get; set; }
        public string Customer_Address2 { get; set; }
        public string Customer_Postcode { get; set; }
        public string Customer_Email { get; set; }
        public string Customer_Phone { get; set; }
        public int Customer_Gender { get; set; }
        public DateTime Customer_Added { get; set; }
        public int Customer_Status { get; set; }

    }
}
