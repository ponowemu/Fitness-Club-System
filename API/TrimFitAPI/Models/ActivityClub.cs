using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("activity_club")]
    public class ActivityClub
    {
        [Key]
        [Column("activity_club_id")]
        public int Activity_Club_Id { get; set; }
        [Column("activity_id")]
        public int Activity_Id { get; set; }
        public Activity Activity { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
        public Club Club { get; set; }
    }
}
