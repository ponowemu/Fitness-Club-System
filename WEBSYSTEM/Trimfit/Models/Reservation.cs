using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Reservation
    {
        public int Reservation_Id { get; set; }
        public int Service_Id { get; set; }
        public int Club_Id { get; set; }
        public DateTime Reservation_From { get; set; }
        public DateTime Reservation_To { get; set; }
        public int Customer_Id { get; set; }
    }
}
