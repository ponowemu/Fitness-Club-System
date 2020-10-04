using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("category")]
    public class Category : IDatabaseEntity
    {
        [Key]
        [Column("category_id")]
        public int Id { get; set; }

        [Column("category_name")]
        public string Name { get; set; }

        [Column("category_color")]
        public string Color { get; set; }

        [Column("category_type")]
        public int Type { get; set; }
    }
}
