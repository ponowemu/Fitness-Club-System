using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("service")]
    public class Service : IDatabaseEntity
    {
        [Key]
        [Column("service_id")]
        public int Id { get; set; }

        [Column("service_name")]
        public string Name { get; set; }

        [Column("employee_id")]
        public List<int> EmployeeId { get; set; }

        [Column("service_net_price")]
        public double NetPrice { get; set; }

        [Column("service_gross_price")]
        public double GrossPrice { get; set; }

        [Column("club_id")]
        public List<int> ClubId { get; set; }

        [Column("service_description")]
        public string Description { get; set; }

        [Column("service_timelimit_mon")]
        public List<TimeSpan> TimelimitMon { get; set; }

        [Column("service_timelimit_tue")]
        public List<TimeSpan> TimelimitTue { get; set; }

        [Column("service_timelimit_wed")]
        public List<TimeSpan> TimelimitWed { get; set; }

        [Column("service_timelimit_thu")]
        public List<TimeSpan> TimelimitThu { get; set; }

        [Column("service_timelimit_fri")]
        public List<TimeSpan> TimelimitFri { get; set; }

        [Column("service_timelimit_sat")]
        public List<TimeSpan> TimelimitSat { get; set; }

        [Column("service_timelimit_sun")]
        public List<TimeSpan> TimelimitSun { get; set; }

        [Column("category_id")]
        public List<int> CategoryId { get; set; }

        [Column("service_duration")]
        public int Duration { get; set; }

        [Column("service_photo_url")]
        public string PhotoUrl { get; set; }

        [Column("service_status")]
        public int Status { get; set; }
    }
}
