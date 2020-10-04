using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("voucher")]
    public class Voucher : IDatabaseEntity
    {
        [Key]
        [Column("voucher_id")]
        public int Id { get; set; }

        [Column("voucher_startdate")]
        public DateTime? Startdate { get; set; }

        [Column("voucher_enddate")]
        public DateTime? Enddate { get; set; }

        [Column("voucher_type_id")]
        public int TypeId { get; set; }

        [Column("voucher_description")]
        public string Description { get; set; }

        [Column("voucher_status")]
        public int Status { get; set; }

        [Column("voucher_entries_number")]
        public int? EntriesNumber { get; set; }

        [Column("voucher_infinity")]
        public bool Infinity { get; set; }

        [Column("voucher_max_suspend_days")]
        public int MaxSuspendDays { get; set; }

        [Column("voucher_max_suspend_times")]
        public int MaxSuspendTimes { get; set; }

        [Column("voucher_net_price")]
        public double NetPrice { get; set; }

        [Column("voucher_gross_price")]
        public double GrossPrice { get; set; }

        [Column("activity_id")]
        public List<int> ActivitiyId { get; set; }

        [Column("voucher_timelimit_mon")]
        public List<DateTime> TimelimitMon { get; set; }

        [Column("voucher_timelimit_tue")]
        public List<DateTime> TimelimitTue { get; set; }

        [Column("voucher_timelimit_wed")]
        public List<DateTime> TimelimitWed { get; set; }

        [Column("voucher_timelimit_thu")]
        public List<DateTime> TimelimitThu { get; set; }

        [Column("voucher_timelimit_fri")]
        public List<DateTime> TimelimitFri { get; set; }

        [Column("voucher_timelimit_sat")]
        public List<DateTime> TimelimitSat { get; set; }

        [Column("voucher_timelimit_sun")]
        public List<DateTime> TimelimitSun { get; set; }

        public IList<Customer> Customers { get; set; }
         
        [ForeignKey("voucher_type_Id")]
        public VoucherType VoucherType { get; set; }

        [Column("voucher_days_number")]
        public int? DaysNumber { get; set; }

    }
}
