using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("position")]
    public class Position
    {
        [Key]
        [Column("position_id")]
        public int Position_Id { get; set; }
        [Column("position_name")]
        public string Position_Name { get; set; }
        [Column("position_status")]
        public bool Position_Status { get; set; }
    }
}
