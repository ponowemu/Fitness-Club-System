using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("user_detail")]
    public class UserDetail : IDatabaseEntity
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("user_detail_card_number")]
        public string DetailCardNumber { get; set; }

        [Column("user_detail_uid")]
        public string DetailUid { get; set; }

        [Column("user_detail_hashid")]
        public string DetailHashid { get; set; }

        [Column("user_detail_prefered_language")]
        public int DetailPreferedLanguage { get; set; }

        [Column("user_detail_agreements")]
        public List<int> DetailAgreements { get; set; }
    }
}
