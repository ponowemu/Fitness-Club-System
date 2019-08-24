using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("voucher_customer")]
    public class VoucherCustomer
    {
        [Key]
        [Column("voucher_customer_id")]
        public int Voucher_Customer_Id { get; set; }
        [Column("customer_id")]
        public int Customer_Id { get; set; }
        public Customer Customer { get; set; }
        [Column("voucher_id")]
        public int Voucher_Id { get; set; }
        public Voucher Voucher { get; set; }
        [Column("voucher_customer_suspend_date")]
        public List<string> Voucher_Customer_Suspend_Date { get; set; }
        [Column("voucher_customer_suspend_number")]
        public int Voucher_Customer_Suspend_Number { get; set; }
    }
}
