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
        [Column("employee_gender")]
        public int Employee_Gender { get; set; }
        [Column("employee_added")]
        public DateTime Employee_Added { get; set; }
        [Column("position_id")]
        public List<int> Position_Id { get; set; }
        [Column("employee_status")]
        public int Employee_Status { get; set; }

        [Column("address_id")]
        public int? Address_Id { get; set; }
        public Address Address { get; set; }

        [Column("employee_display_name")]
        public string Employee_Display_Name { get; set; }
        [Column("employee_photo_url")]
        public string Employee_Photo_Url { get; set; }

        [Column("user_id")]
        public int? User_id { get; set; }
        public User User { get; set; }
    }
}
