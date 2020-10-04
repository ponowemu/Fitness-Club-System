using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("reservation")]
    public class Reservation : IDatabaseEntity
    {
        [Key]
        [Column("reservation_id")]
        public int Id { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }

        [Column("reservation_from")]
        public DateTime DateFrom { get; set; }

        [Column("reservation_to")]
        public DateTime DateTo { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("payment_id")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
