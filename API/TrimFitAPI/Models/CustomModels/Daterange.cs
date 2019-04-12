using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models.CustomModels
{
    [NotMapped]
    public class Daterange
    {
        [NotMapped]
        public DateTime Start_Date { get; set; }
        [NotMapped]
        public DateTime End_Date { get; set; }

    }
}
