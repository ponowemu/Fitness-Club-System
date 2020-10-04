using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("employee")]
    public class Employee : IDatabaseEntity
    {
        [Key]
        [Column("employee_id")]
        public int Id { get; set; }

        [Column("employee_firstname")]
        public string Firstname { get; set; }

        [Column("employee_lastname")]
        public string Lastname { get; set; }

        [Column("employee_birthdate")]
        public DateTime Birthdate { get; set; }

        [Column("employee_gender")]
        public int Gender { get; set; }

        [Column("employee_added")]
        public DateTime Added { get; set; }

        [Column("position_id")]
        public List<int> PositionId { get; set; }

        [Column("employee_status")]
        public int Status { get; set; }

        [Column("address_id")]
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Column("employee_display_name")]
        public string DisplayName { get; set; }
        [Column("employee_photo_url")]
        public string PhotoUrl { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
