using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class VoucherCustomer
    {
        [JsonProperty(PropertyName ="voucher_customer_id")]
        public int Voucher_Customer_Id { get; set; }
        [JsonProperty(PropertyName ="customer_id")]
        public int Customer_Id { get; set; }
        [JsonProperty(PropertyName ="voucher_id")]
        public int Voucher_Id { get; set; }
        [JsonProperty(PropertyName ="voucher_customer_suspend_date")]
        public List<string> Voucher_Customer_Suspend_Date { get; set; }
        [JsonProperty(PropertyName ="voucher_customer_suspend_number")]
        public int Voucher_Customer_Suspend_Number { get; set; }
    }
}
