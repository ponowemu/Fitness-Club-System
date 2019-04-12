using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models.CustomModels
{
    public class Daterange
    {
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        public Daterange(List<DateTime> dates)
        {
            this.Start_Date = dates.FirstOrDefault();
            this.End_Date = dates.LastOrDefault();
        }
    }
}
