using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("activity_club")]
    public class ActivityClub : IDatabaseEntity
    {
        [Key]
        [Column("activity_club_id")]
        public int Id { get; set; }

        [Column("activity_id")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
