using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int Customer_Id {get;set;}
        [Column("customer_firstname")]
        public string Customer_Firstname { get; set; }
        [Column("customer_lastname")]
        public string Customer_Lastname { get; set; }
        [Column("customer_birthdate")]
        public DateTime Customer_Birthdate { get; set; }
        [Column("customer_gender")]
        public int Customer_Gender { get; set; }
        [Column("customer_added")]
        public DateTime Customer_Added { get; set; }
        [Column("customer_status")]
        public int Customer_Status { get; set; }
        [Column("customer_isconfirmed")]
        public bool Customer_Isconfirmed { get; set; }

        [Column("address_id")]
        public int Address_Id { get; set; }
        public Address Address { get; set; }

        [Column("customer_display_name")]
        public string Customer_Display_Name { get; set; }
        [Column("customer_photo_url")]
        public string Customer_Photo_Url { get; set; }

    }
}
