using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("reservation")]
    public class Reservation
    {
        [Key]
        [Column("reservation_id")]
        public int Reservation_Id { get; set; }
        [Column("service_id")]
        public int Service_Id { get; set; }
        public Service Service { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
        public Club Club { get; set; }
        [Column("reservation_from")]
        public DateTime Reservation_From { get; set; }
        [Column("reservation_to")]
        public DateTime Reservation_To { get; set; }
    }
}
