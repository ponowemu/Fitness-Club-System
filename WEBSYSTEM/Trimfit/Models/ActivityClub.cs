using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class ActivityClub
    {
      
        public int Activity_Club_Id { get; set; }
        public int Activity_Id { get; set; }
        public int Club_Id { get; set; }
    }
}
