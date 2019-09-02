using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        [Column("payment_id")]
        public int Payment_Id { get; set; }
        [Column("customer_id")]
        public int Customer_Id { get; set; }
        public Customer Customer { get; set; }
        [Column("payment_type_id")]
        public int Payment_Type_Id {get;set;}
        [Column("payment_status")]
        public int Payment_Status { get; set; }
        [Column("payment_time")]
        public DateTime Payment_Time { get; set; }
    }
}
