using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("employee_club")]
    public class EmployeeClub
    {
        [Key]
        [Column("employee_club_id")]
        public int Employee_Club_Id { get; set; }
        [Column("employee_id")]
        public int Employee_Id { get; set; }
        [Column("club_id")]
        public int Club_Id { get; set; }
    }
}
