using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("payment")]
    public class Payment : IDatabaseEntity
    {
        [Key]
        [Column("payment_id")]
        public int Id { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("payment_type_id")]
        public int PaymentTypeId { get; set; }

        [Column("payment_status")]
        public int Status { get; set; }

        [Column("payment_time")]
        public DateTime Time { get; set; }
    }
}
