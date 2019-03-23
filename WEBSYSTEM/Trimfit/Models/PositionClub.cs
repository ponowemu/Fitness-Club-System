using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class PositionClub
    {
        public int Position_Club_Id { get; set; }
        public int Position_Id { get; set; }
        public int Club_Id { get; set; }
    }
}
