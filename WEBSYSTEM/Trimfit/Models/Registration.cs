using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Registration
    {
        public int Registration_Id { get; set; }
        public int Timetable_Id { get; set; }
        public int Customer_Id { get; set; }
        public DateTime Registartion_Created { get; set; }
        public int Payment_Id { get; set; }
        public int Registration_Type { get; set; }

    }
}
