using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("position_club")]
    public class PositionClub : IDatabaseEntity
    {
        [Key]
        [Column("position_club_id")]
        public int Id { get; set; }

        [Column("position_id")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
