using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Class
    {
        [Key]
        public long Class_id { get; set; }
        public string Class_name { get; set; }
        public string Class_description { get; set; }
        public bool Class_active { get; set; }
    }
}
