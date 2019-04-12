using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("voucher_type")]
    public class VoucherType
    {
        [Key]
        [Column("voucher_type_id")]
        public int Voucher_Type_Id { get; set; }
        [Column("voucher_type_name")]
        public string Voucher_Type_Name { get; set; }
        [Column("voucher_type_description")]
        public string Voucher_Type_Description { get; set; }
        [Column("voucher_type_parameters")]
        public string Voucher_Type_Parameters { get; set; }
        [Column("voucher_type_parameters2")]
        public string Voucher_Type_Parameters2 { get; set; }
    }
}
