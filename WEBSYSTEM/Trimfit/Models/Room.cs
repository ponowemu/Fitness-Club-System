using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    public class Room
    {
        public int Room_Id { get; set; }
        public string Room_Name { get; set; }
        public string Room_Descripition { get; set; }
    }
}
