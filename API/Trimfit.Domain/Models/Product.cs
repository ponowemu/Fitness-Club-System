using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("product")]
    public class Product : IDatabaseEntity
    {
        [Key]
        [Column("product_id")]
        public int Id { get; set; }
        [Column("product_name")]
        public string Name { get; set; }
        [Column("product_net_price")]
        public decimal NetPrice { get; set; }
        [Column("product_gross_price")]
        public decimal GrossPrice { get; set; }
        [Column("club_id")]
        public List<int> ClubId { get; set; }
        [Column("product_quantity")]
        public int Quantity { get; set; }
        [Column("product_status")]
        public int Status { get; set; }
        [Column("category_id")]
        public List<int> CategoryId { get; set; }
        [Column("product_icon")]
        public string Icon { get; set; }
    }
}
