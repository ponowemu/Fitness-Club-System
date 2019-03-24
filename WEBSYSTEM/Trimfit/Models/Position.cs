using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Position
    {
        public int Position_Id { get; set; }
        public string Position_Name { get; set; }
        public bool Position_Status { get; set; }
    }
}
