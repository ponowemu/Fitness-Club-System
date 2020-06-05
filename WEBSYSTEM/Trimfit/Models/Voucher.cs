using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Voucher
    {
        public Voucher()
        {
            Activities = new List<Activity>();
        }
        [JsonProperty(PropertyName ="voucher_id")]
        public int? Voucher_Id { get; set; }
        [JsonProperty(PropertyName ="voucher_startdate")]
        public DateTime? Voucher_Startdate { get; set; }
        [JsonProperty(PropertyName ="voucher_enddate")]
        public DateTime? Voucher_Enddate { get; set; }
        [JsonProperty(PropertyName ="voucher_type_id")]
        public int Voucher_Type_Id { get; set; }
        [JsonProperty(PropertyName ="voucher_description")]
        public string Voucher_Description { get; set; }
        [JsonProperty(PropertyName ="voucher_status")]
        public int Voucher_Status { get; set; }
        [JsonProperty(PropertyName ="voucher_entries_number")]
        public int? Voucher_Entries_Number { get; set; }
        [JsonProperty(PropertyName ="voucher_infinity")]
        public bool Voucher_Infinity { get; set; }
        [JsonProperty(PropertyName ="voucher_max_suspend_days")]
        public int Voucher_Max_Suspend_Days { get; set; }
        [JsonProperty(PropertyName ="voucher_max_suspend_times")]
        public int Voucher_Max_Suspend_Times { get; set; }
        [JsonProperty(PropertyName ="voucher_net_price")]
        public decimal Voucher_Net_Price { get; set; }
        [JsonProperty(PropertyName ="voucher_gross_price")]
        public decimal Voucher_Gross_Price { get; set; }
        [JsonProperty(PropertyName ="activitiy_id")]
        public List<int> Activitiy_Id { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_mon")]
        public List<DateTime> Voucher_Timelimit_Mon { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_tue")]
        public List<DateTime> Voucher_Timelimit_Tue { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_wed")]
        public List<DateTime> Voucher_Timelimit_Wed { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_thu")]
        public List<DateTime> Voucher_Timelimit_Thu { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_fri")]
        public List<DateTime> Voucher_Timelimit_Fri { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_sat")]
        public List<DateTime> Voucher_Timelimit_Sat { get; set; }
        [JsonProperty(PropertyName ="voucher_timelimit_sun")]
        public List<DateTime> Voucher_Timelimit_Sun { get; set; }
        [JsonProperty(PropertyName = "voucherType")]
        public VoucherType VoucherType { get; set; }
        public List<Activity> Activities { get; set; }
        [JsonProperty(PropertyName = "voucherDaysNumber")]
        public int? VoucherDaysNumber { get; set; }

    }
}
