using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("voucher")]
    public class Voucher
    {
        [Key]
        [Column("voucher_id")]
        public int Voucher_Id { get; set; }
        [Column("voucher_customer_id")]
        public string Voucher_Customer_Id { get; set; }
        [Column("voucher_startdate")]
        public DateTime Voucher_Startdate { get; set; }
        [Column("voucher_enddate")]
        public DateTime Voucher_Enddate { get; set; }
        [Column("voucher_type_id")]
        public int Voucher_Type_Id { get; set; }
        [Column("voucher_description")]
        public string Voucher_Description { get; set; }
        [Column("voucher_status")]
        public int Voucher_Status { get; set; }
    }
}
