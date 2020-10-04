using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("registration")]
    public class Registration : IDatabaseEntity
    {
        [Key]
        [Column("registration_id")]
        public int Id { get; set; }

        [Column("timetable_activity_id")]
        public int TimetableActivityId { get; set; }

        public TimetableActivity TimetableActivity { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("registration_created")]
        public DateTime RegistartionCreated { get; set; }

        [Column("payment_id")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [Column("registration_type")]
        public int RegistrationType { get; set; }
    }
}
