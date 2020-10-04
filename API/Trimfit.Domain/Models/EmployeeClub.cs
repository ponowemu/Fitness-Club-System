using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("employee_club")]
    public class EmployeeClub : IDatabaseEntity
    {
        [Key]
        [Column("employee_club_id")]
        public int Id { get; set; }

        [Column("employee_id")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Column("club_id")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
