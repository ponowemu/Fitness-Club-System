using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("user_detail")]
    public class UserDetail
    {
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("user_detail_card_number")]
        public string User_Detail_Card_Number { get; set; }
        [Column("user_detail_uid")]
        public string User_Detail_Uid { get; set; }
        [Column("user_detail_hashid")]
        public string User_Detail_Hashid { get; set; }
        [Column("user_detail_prefered_language")]
        public int User_Detail_Prefered_Language { get; set; }
        [Column("user_detail_agreements")]
        public List<int> User_Detail_Agreements { get; set; }
    }
}
