using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("position")]
    public class Position : IDatabaseEntity
    {
        [Key]
        [Column("position_id")]
        public int Id { get; set; }

        [Column("position_name")]
        public string PositionName { get; set; }

        [Column("position_status")]
        public bool PositionStatus { get; set; }
    }
}
