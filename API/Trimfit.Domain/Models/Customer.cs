using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("customer")]
    public sealed class Customer : IDatabaseEntity
    {
        [Key]
        [Column("customer_id")]
        public int Id { get; set; }

        [Column("customer_firstname")]
        public string FirstName { get; set; }

        [Column("customer_lastname")]
        public string LastName { get; set; }

        [Column("customer_birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("customer_gender")]
        public int Gender { get; set; }

        [Column("customer_added")]
        public DateTime DateAdded { get; set; }

        [Column("customer_status")]
        public int Status { get; set; }

        [Column("customer_isconfirmed")]
        public bool IsConfirmed { get; set; }

        [Column("address_id")]
        [ForeignKey("Adress")]
        public int? AddressId { get; set; }
        public Address Address { get; set; }

        [Column("customer_display_name")]
        public string DisplayName { get; set; }

        [Column("customer_photo_url")]
        public string PhotoUrl { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<VoucherCustomer> Vouchers { get; set; }
        public ICollection<CustomerClub> Clubs { get; set; }

    }
}
