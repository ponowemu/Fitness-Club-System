using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int Category_Id { get; set; }
        [Column("category_name")]
        public string Category_Name { get; set; }
        [Column("category_color")]
        public string Category_Color { get; set; }
    }
}
