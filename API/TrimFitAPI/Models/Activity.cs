﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("activity")]
    public class Activity
    {
        [Key]
        [Column("activity_id")]
        public int Activity_Id { get; set; }
        [Column("activity_name")]
        public string Activity_Name { get; set; }
        [Column("activity_description")]
        public string Activity_Description { get; set; }
        [Column("activity_status")]
        public int Activity_Status { get; set; }
        [Column("category_id")]
        public int Category_Id { get; set; }
        [Column("activity_color")]
        public string Activity_Color { get; set; }
        [Column("employee_id")]
        public List<int> Employee_Id { get; set; }
    }
}
