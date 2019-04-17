using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Service
    {
        [JsonProperty(PropertyName ="service_id")]
        public int Service_Id { get; set; }
        [JsonProperty(PropertyName ="service_name")]
        public string Service_Name { get; set; }
        [JsonProperty(PropertyName ="employee_id")]
        public List<int> Employee_Id { get; set; }
        [JsonProperty(PropertyName ="service_net_price")]
        public decimal Service_Net_Price { get; set; }
        [JsonProperty(PropertyName ="service_gross_price")]
        public decimal Service_Gross_Price { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public List<int> Club_Id { get; set; }
        [JsonProperty(PropertyName ="service_description")]
        public string Service_Description { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_mon")]
        public List<TimeSpan> Service_Timelimit_Mon { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_tue")]
        public List<TimeSpan> Service_Timelimit_Tue { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_wed")]
        public List<TimeSpan> Service_Timelimit_Wed { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_thu")]
        public List<TimeSpan> Service_Timelimit_Thu { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_fri")]
        public List<TimeSpan> Service_Timelimit_Fri { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_sat")]
        public List<TimeSpan> Service_Timelimit_Sat { get; set; }
        [JsonProperty(PropertyName ="service_timelimit_sun")]
        public List<TimeSpan> Service_Timelimit_Sun { get; set; }
        [JsonProperty(PropertyName = "category_id")]
        public List<int> Category_Id { get; set; }
        [JsonProperty(PropertyName ="service_duration")]
        public int Service_Duration { get; set; }
    }
}
