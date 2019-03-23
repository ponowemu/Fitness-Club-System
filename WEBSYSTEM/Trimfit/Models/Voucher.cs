using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Voucher
    {

        public int Voucher_Id { get; set; }
        public string Voucher_Customer_Id { get; set; }
        public DateTime Voucher_Startdate { get; set; }
        public DateTime Voucher_Enddate { get; set; }
        public int Voucher_Type_Id { get; set; }
        public string Voucher_Description { get; set; }
        public int Voucher_Status { get; set; }
    }
}
