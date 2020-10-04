using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("room_club")]
    public class RoomClub : IDatabaseEntity
    {
        [Key]
        [Column("room_club_id")]
        public int Id { get; set; }

        [Column("room_id")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
