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
        [Column("club_address1")]
        public string Club_Address1 { get; set; }
        [Column("club_address2")]
        public string Club_Address2 { get; set; }
        [Column("club_postcode")]
        public string Club_Postcode { get; set; }
        [Column("club_email")]
        public string Club_Email { get; set; }
        [Column("club_phone")]
        public string Club_Phone {get;set;}
        [Column("club_status")]
        public int Club_Status { get; set; }
    }
}
