using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrimFitAPI.Models.CustomModels;

namespace TrimFitAPI.Models
{
    [Table("service")]
    public class Service
    {
        [Key]
        [Column("service_id")]
        public int Service_Id { get; set; }
        [Column("service_name")]
        public string Service_Name { get; set; }
        [Column("employee_id")]
        public List<int> Employee_Id { get; set; }
        [Column("service_net_price")]
        public double Service_Net_Price { get; set; }
        [Column("service_gross_price")]
        public double Service_Gross_Price { get; set; }
        [Column("club_id")]
        public List<int> Club_Id { get; set; }
        [Column("service_description")]
        public string Service_Description { get; set; }
        [Column("service_timelimit_mon")]
        public List<DateTime> Service_Timelimit_Mon { get; set; }
        [Column("service_timelimit_tue")]
        public List<DateTime> Service_Timelimit_Tue { get; set; }
        [Column("service_timelimit_wed")]
        public List<DateTime> Service_Timelimit_Wed { get; set; }
        [Column("service_timelimit_thu")]
        public List<DateTime> Service_Timelimit_Thu { get; set; }
        [Column("service_timelimit_fri")]
        public List<DateTime> Service_Timelimit_Fri { get; set; }
        [Column("service_timelimit_sat")]
        public List<DateTime> Service_Timelimit_Sat { get; set; }
        [Column("service_timelimit_sun")]
        public List<DateTime> Service_Timelimit_Sun { get; set; }
    }
}
