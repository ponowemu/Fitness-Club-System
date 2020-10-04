using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("voucher_customer")]
    public class VoucherCustomer : IDatabaseEntity
    {
        [Key]
        [Column("voucher_customer_id")]
        public int Id { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Column("voucher_id")]
        public int VoucherId { get; set; }

        public Voucher Voucher { get; set; }

        [Column("voucher_customer_suspend_date")]
        public List<string> SuspendDate { get; set; }

        [Column("voucher_customer_suspend_number")]
        public int SuspendNumber { get; set; }

        [Column("voucher_end_date")]
        public DateTime? VoucherEndDate { get; set; }

        [Column("voucher_free_entries")]
        public int? FreeEntries { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; }

        [Column("added")]
        public DateTime? Added { get; set; }
    }
}
