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
        [Column("customer_address1")]
        public string Customer_Address1 { get; set; }
        [Column("customer_address2")]
        public string Customer_Address2 { get; set; }
        [Column("customer_postcode")]
        public string Customer_Postcode { get; set; }
        [Column("customer_email")]
        public string Customer_Email { get; set; }
        [Column("customer_phone")]
        public string Customer_Phone { get; set; }
        [Column("customer_gender")]
        public int Customer_Gender { get; set; }
        [Column("customer_added")]
        public DateTime Customer_Added { get; set; }
        [Column("customer_status")]
        public int Customer_Status { get; set; }

    }
}
