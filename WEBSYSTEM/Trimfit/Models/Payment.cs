using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Payment
    {
        public int Payment_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Payment_Type_Id {get;set;}
        public int Payment_Status { get; set; }
        public DateTime Payment_Time { get; set; }
    }
}
