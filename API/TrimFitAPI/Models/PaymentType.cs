using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("payment_type")]
    public class PaymentType
    {
        [Key]
        [Column("payment_type_id")]
        public int Payment_Type_Id { get; set; }
        [Column("payment_type_name")]
        public string Payment_Type_Name { get; set; }
        [Column("payment_type_description")]
        public string Payment_Type_Description { get; set; }
        [Column("payment_type_status")]
        public Boolean Payment_Type_Status { get; set; }
    }
}
