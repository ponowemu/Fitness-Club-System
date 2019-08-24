using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("registration")]
    public class Registration
    {
        [Key]
        [Column("registration_id")]
        public int Registration_Id { get; set; }
        [Column("timetable_activity_id")]
        public int Timetable_Activity_Id { get; set; }
        public TimetableActivity TimetableActivity { get; set; }
        [Column("customer_id")]
        public int Customer_Id { get; set; }
        public Customer Customer { get; set; }
        [Column("registration_created")]
        public DateTime Registartion_Created { get; set; }
        [Column("payment_id")]
        public int Payment_Id { get; set; }
        public Payment Payment { get; set; }
        [Column("registration_type")]
        public int Registration_Type { get; set; }

    }
}
