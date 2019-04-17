using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int Product_Id { get; set; }
        [Column("product_name")]
        public string Product_Name { get; set; }
        [Column("product_net_price")]
        public decimal Product_Net_Price { get; set; }
        [Column("product_gross_price")]
        public decimal Product_Gross_Price { get; set; }
        [Column("club_id")]
        public List<int> Club_Id { get; set; }
        [Column("product_quantity")]
        public int Product_Quantity { get; set; }
        [Column("product_status")]
        public int Product_Status { get; set; }
        [Column("category_id")]
        public List<int> Category_Id { get; set; }
        [Column("product_icon")]
        public string Product_Icon { get; set; }
    }
}
