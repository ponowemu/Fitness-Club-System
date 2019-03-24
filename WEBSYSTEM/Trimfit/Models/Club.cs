using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Club
    {
        public int Club_Id { get; set; }
        public string Club_Name { get; set; }
        public string Club_Address1 { get; set; }
        public string Club_Address2 { get; set; }
        public string Club_Postcode { get; set; }
        public string Club_Email { get; set; }
        public string Club_Phone {get;set;}
        public int Club_Status { get; set; }
    }
}
