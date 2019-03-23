using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class VoucherType
    {
        public int Voucher_Id { get; set; }
        public string Voucher_Type_Name { get; set; }
        public string Voucher_Type_Description { get; set; }
        public string Voucher_Type_Parameters { get; set; }
    }
}
