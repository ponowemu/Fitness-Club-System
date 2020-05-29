using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("voucher")]
    public class Voucher
    {
        [Key]
        [Column("voucher_id")]
        public int Voucher_Id { get; set; }
        [Column("voucher_startdate")]
        public DateTime Voucher_Startdate { get; set; }
        [Column("voucher_enddate")]
        public DateTime Voucher_Enddate { get; set; }
        [Column("voucher_type_id")]
        public int Voucher_Type_Id { get; set; }
        [Column("voucher_description")]
        public string Voucher_Description { get; set; }
        [Column("voucher_status")]
        public int Voucher_Status { get; set; }
        [Column("voucher_entries_number")]
        public int Voucher_Entries_Number { get; set; }
        [Column("voucher_infinity")]
        public bool Voucher_Infinity { get; set; }
        [Column("voucher_max_suspend_days")]
        public int Voucher_Max_Suspend_Days { get; set; }
        [Column("voucher_max_suspend_times")]
        public int Voucher_Max_Suspend_Times { get; set; }
        [Column("voucher_net_price")]
        public double Voucher_Net_Price { get; set; }
        [Column("voucher_gross_price")]
        public double Voucher_Gross_Price { get; set; }
        [Column("activity_id")]
        public List<int> Activitiy_Id { get; set; }
        [Column("voucher_timelimit_mon")]
        public List<DateTime> Voucher_Timelimit_Mon { get; set; }
        [Column("voucher_timelimit_tue")]
        public List<DateTime> Voucher_Timelimit_Tue { get; set; }
        [Column("voucher_timelimit_wed")]
        public List<DateTime> Voucher_Timelimit_Wed { get; set; }
        [Column("voucher_timelimit_thu")]
        public List<DateTime> Voucher_Timelimit_Thu { get; set; }
        [Column("voucher_timelimit_fri")]
        public List<DateTime> Voucher_Timelimit_Fri { get; set; }
        [Column("voucher_timelimit_sat")]
        public List<DateTime> Voucher_Timelimit_Sat { get; set; }
        [Column("voucher_timelimit_sun")]
        public List<DateTime> Voucher_Timelimit_Sun { get; set; }

        public IList<VoucherCustomer> Customers { get; set; }
         
        [ForeignKey("Voucher_Type_Id")]
        public VoucherType VoucherType { get; set; }

    }
}
