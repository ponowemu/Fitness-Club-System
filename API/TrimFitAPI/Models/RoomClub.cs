using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("room_club")]
    public class RoomClub
    {
        [Key]
        [Column("room_club_id")]
        public int Room_Club_Id { get; set; }
        [Column("room_id")]
        public int Room_Id { get; set; }
        public Room Room { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
        public Club Club { get; set; }
    }
}
