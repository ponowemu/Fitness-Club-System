using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrimFitAPI.Models
{
    [Table("address")]
    public class Address
    {
        [Column("address_id")]
        public int Address_Id { get; set; }
        [Column("address_1")]
        public string Address_1 { get; set; }
        [Column("address_2")]
        public string Address_2 { get; set; }
        [Column("address_city")]
        public string Address_City { get; set; }
        [Column("address_country")]
        public string Address_Country { get; set; }
        [Column("address_email")]
        public string Address_Email { get; set; }
        [Column("address_postcode")]
        public string Address_Postcode { get; set; }
        [Column("address_phone")]
        public string Address_Phone { get; set; }
    }
}
