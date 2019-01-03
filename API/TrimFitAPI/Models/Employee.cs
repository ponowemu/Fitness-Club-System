using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public int Employee_Id { get; set; }
        [Column("employee_firstname")]
        public string Employee_Firstname { get; set; }
        [Column("employee_lastname")]
        public string Employee_Lastname { get; set; }
        [Column("employee_birthdate")]
        public DateTime Employee_Birthdate { get; set; }
        [Column("employee_address1")]
        public string Employee_Address1 { get; set; }
        [Column("employee_address2")]
        public string Employee_Address2 { get; set; }
        [Column("employee_postcode")]
        public string Employee_Postcode { get; set; }
        [Column("employee_email")]
        public string Employee_Email { get; set; }
        [Column("employee_phone")]
        public string Employee_Phone { get; set; }
        [Column("employee_gender")]
        public int Employee_Gender { get; set; }
        [Column("employee_added")]
        public DateTime Employee_Added { get; set; }
        [Column("employee_position_id")]
        public int Employee_Position_Id { get; set; }
        [Column("employee_status")]
        public int Employee_Status { get; set; }
    }
}
