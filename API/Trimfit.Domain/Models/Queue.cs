using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("queue")]
    public class Queue : IDatabaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
