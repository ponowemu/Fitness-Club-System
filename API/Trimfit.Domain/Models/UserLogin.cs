using System.ComponentModel.DataAnnotations.Schema;

namespace Trimfit.Domain.Models
{
    public class UserLogin
    {
        [Column("user_login")]
        public string Login { get; set; }
        [Column("user_password")]
        public virtual string Password { get; set; }
    }
}
