using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class ServiceView
    {

        public int Service_Id { get; set; }
        public string Service_Name { get; set; }
        public List<Employee> Employee { get; set; }
        public decimal Service_Net_Price { get; set; }
        public decimal Service_Gross_Price { get; set; }
        public List<Club> Club { get; set; }
        public string Service_Description { get; set; }
        public List<TimeSpan> Service_Timelimit_Mon { get; set; }
        public List<TimeSpan> Service_Timelimit_Tue { get; set; }
        public List<TimeSpan> Service_Timelimit_Wed { get; set; }
        public List<TimeSpan> Service_Timelimit_Thu { get; set; }
        public List<TimeSpan> Service_Timelimit_Fri { get; set; }
        public List<TimeSpan> Service_Timelimit_Sat { get; set; }
        public List<TimeSpan> Service_Timelimit_Sun { get; set; }
        public List<Category> Category { get; set; }
        public int Service_Duration { get; set; }
        public string Service_Photo_Url { get; set; }
    }
}
