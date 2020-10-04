using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("address")]
    public class Address : IDatabaseEntity
    {
        [Key]
        [Column("address_id")]
        public int Id { get; set; }

        [Column("address_1")]
        public string Address1 { get; set; }

        [Column("address_2")]
        public string Address2 { get; set; }

        [Column("address_city")]
        public string City { get; set; }

        [Column("address_country")]
        public string Country { get; set; }

        [Column("address_email")]
        public string Email { get; set; }

        [Column("address_postcode")]
        public string Postcode { get; set; }

        [Column("address_phone")]
        public string Phone { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Models.Employee.Address))]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Models.Customer.Address))]
        public virtual Customer Customer { get; set; }
    }
}
