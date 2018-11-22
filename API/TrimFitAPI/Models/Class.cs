using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("tf_class")]
    public class Class
    {
        [Key]
        [Column("class_id")]
        public long Class_id { get; set; }
        [Column("class_name")]
        public string Class_name { get; set; }
        [Column("class_description")]
        public string Class_description { get; set; }
        [Column("class_active")]
        public bool Class_active { get; set; }
    }
}
