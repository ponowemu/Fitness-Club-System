using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("voucher_type")]
    public class VoucherType : IDatabaseEntity
    {
        [Key]
        [Column("voucher_type_id")]
        public int Id { get; set; }
        [Column("voucher_type_name")]
        public string Name { get; set; }
        [Column("voucher_type_description")]
        public string Description { get; set; }
        [Column("voucher_type_parameters")]
        public string Parameters { get; set; }
        [Column("voucher_type_parameters2")]
        public string Parameters2 { get; set; }
    }
}
