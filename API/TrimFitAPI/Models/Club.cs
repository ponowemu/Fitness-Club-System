using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("club")]
    public class Club
    {
        [Key]
        [Column("club_id")]
        public int Club_Id { get; set; }
        [Column("club_name")]
        public string Club_Name { get; set; }
        [Column("club_status")]
        public int Club_Status { get; set; }
        [Column("address_id")]
        public int? Address_Id { get; set; }
        public Address Address { get; set; }
    }
}
