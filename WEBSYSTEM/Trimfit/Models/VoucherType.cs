using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class VoucherType
    {
        [JsonProperty(PropertyName ="voucher_type_id")]
        public int Voucher_Type_Id { get; set; }
        [JsonProperty(PropertyName ="voucher_type_name")]
        public string Voucher_Type_Name { get; set; }
        [JsonProperty(PropertyName ="voucher_type_description")]
        public string Voucher_Type_Description { get; set; }
        [JsonProperty(PropertyName ="voucher_type_parameters")]
        public string Voucher_Type_Parameters { get; set; }
        [JsonProperty(PropertyName ="voucher_type_parameters2")]
        public string Voucher_Type_Parameters2 { get; set; }
    }
}
