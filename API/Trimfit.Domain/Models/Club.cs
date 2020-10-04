using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("club")]
    public sealed class Club : IDatabaseEntity
    {
        [Key]
        [Column("club_id")]
        public int Id { get; set; }

        [Column("club_name")]
        public string Name { get; set; }

        [Column("club_status")]
        public int Status { get; set; }

        [Column("address_id")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IList<EmployeeClub> Employees { get; set; }
        public IList<CustomerClub> Customers { get; set; }
    }
}
