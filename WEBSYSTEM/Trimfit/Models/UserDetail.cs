using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class UserDetail
    {
        [JsonProperty(PropertyName ="user_id")]
        public int User_Id { get; set; }
        [JsonProperty(PropertyName ="user_detail_card_number")]
        public string User_Detail_Card_Number { get; set; }
        [JsonProperty(PropertyName ="user_detail_uid")]
        public string User_Detail_Uid { get; set; }
        [JsonProperty(PropertyName ="user_detail_hashid")]
        public string User_Detail_Hashid { get; set; }
        [JsonProperty(PropertyName ="user_detail_prefered_language")]
        public int User_Detail_Prefered_Language { get; set; }
        [JsonProperty(PropertyName ="user_detail_agreements")]
        public List<int> User_Detail_Agreements { get; set; }
    }
}
