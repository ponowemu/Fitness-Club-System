using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("customer_club")]
    public class CustomerClub : IDatabaseEntity
    {
        [Key]
        [Column("customer_club_id")]
        public int Id { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
