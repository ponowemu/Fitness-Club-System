using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("activity")]
    public class Activity
    {
        [Key]
        [Column("activity_id")]
        private int Activity_Id { get; set; }
        [Column("activity_name")]
        private string Activity_Name { get; set; }
        [Column("activity_description")]
        private string Activity_Description { get; set; }
        [Column("activity_status")]
        private int Activity_Status { get; set; }
    }
}
