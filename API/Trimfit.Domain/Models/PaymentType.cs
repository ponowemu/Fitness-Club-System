using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("payment_type")]
    public class PaymentType : IDatabaseEntity
    {
        [Key]
        [Column("payment_type_id")]
        public int Id { get; set; }

        [Column("payment_type_name")]
        public string Name { get; set; }

        [Column("payment_type_description")]
        public string Description { get; set; }

        [Column("payment_type_status")]
        public bool Status { get; set; }
    }
}
