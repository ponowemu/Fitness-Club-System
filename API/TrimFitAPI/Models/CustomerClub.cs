using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("customer_club")]
    public class CustomerClub
    {
        [Key]
        [Column("customer_club_id")]
        public int Customer_Club_Id { get; set; }
        [Column("customer_id")]
        public int Customer_Id { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
    }
}
