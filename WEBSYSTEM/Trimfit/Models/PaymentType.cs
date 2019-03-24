using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class PaymentType
    {
        public int Payment_Type_Id { get; set; }
        public string Payment_Type_Name { get; set; }
        public string Payment_Type_Description { get; set; }
        public Boolean Payment_Type_Status { get; set; }
    }
}
