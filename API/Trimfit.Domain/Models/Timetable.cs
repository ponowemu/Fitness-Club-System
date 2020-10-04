using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("timetable")]
    public class Timetable : IDatabaseEntity
    {
        [Key]
        [Column("timetable_id")]
        public int Id { get; set; }

        [Column("timetable_name")]
        public string Name { get; set; }

        [Column("timetable_status")]
        public int Status { get; set; }

        [Column("timetable_created")]
        public DateTime Created { get; set; }

        [Column("timetable_edited")]
        public DateTime Edited { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
