using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("position_club")]
    public class PositionClub
    {
        [Key]
        [Column("position_club_id")]
        public int Position_Club_Id { get; set; }
        [Column("position_id")]
        public int Position_Id { get; set; }
        public virtual Position Position { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
        public virtual Club Club { get; set; }
    }
}
