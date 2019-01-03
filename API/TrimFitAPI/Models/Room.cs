using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("room")]
    public class Room
    {
        [Key]
        [Column("room_id")]
        public int Room_Id { get; set; }
        [Column("room_name")]
        public string Room_Name { get; set; }
        [Column("room_description")]
        public string Room_Descripition { get; set; }
    }
}
