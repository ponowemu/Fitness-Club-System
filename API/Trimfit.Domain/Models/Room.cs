using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("room")]
    public class Room : IDatabaseEntity
    {
        [Key]
        [Column("room_id")]
        public int Id { get; set; }

        [Column("room_name")]
        public string Name { get; set; }

        [Column("room_description")]
        public string Description { get; set; }
    }
}
