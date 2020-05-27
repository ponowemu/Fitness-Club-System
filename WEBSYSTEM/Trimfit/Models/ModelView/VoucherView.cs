using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models.ModelView
{
    public class VoucherView
    {
        public int Voucher_Id { get; set; }
        public DateTime Voucher_Startdate { get; set; }
        public DateTime Voucher_Enddate { get; set; }
        public VoucherType VoucherType{ get; set; }
        public string Voucher_Description { get; set; }
        public int Voucher_Status { get; set; }
        public int Voucher_Entries_Number { get; set; }
        public bool Voucher_Infinity { get; set; }
        public int Voucher_Max_Suspend_Days { get; set; }
        public int Voucher_Max_Suspend_Times { get; set; }
        public decimal Voucher_Net_Price { get; set; }
        public decimal Voucher_Gross_Price { get; set; }
        public List<Activity> Activities { get; set; }
        public List<DateTime> Voucher_Timelimit_Mon { get; set; }
        public List<DateTime> Voucher_Timelimit_Tue { get; set; }
        public List<DateTime> Voucher_Timelimit_Wed { get; set; }
        public List<DateTime> Voucher_Timelimit_Thu { get; set; }
        public List<DateTime> Voucher_Timelimit_Fri { get; set; }
        public List<DateTime> Voucher_Timelimit_Sat { get; set; }
        public List<DateTime> Voucher_Timelimit_Sun { get; set; }
    }
}
