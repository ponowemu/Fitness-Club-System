using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("user")]
    public class User : UserLogin, IDatabaseEntity
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("user_token")]
        public string Token { get; set; }

        [Column("user_type")]
        public int Type { get; set; }

        [Column("user_status")]
        public int Status { get; set; }

        [Column("user_photo_url")]
        public string PhotoUrl { get; set; }

        [Column("user_firstname")]
        public string FirstName { get; set; }

        [Column("user_lastname")]
        public string LastName { get; set; }

        [JsonIgnore]
        public override string Password { get => base.Password; set => base.Password = value; }
    }
}
