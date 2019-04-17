using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class Product
    {
        [JsonProperty(PropertyName ="product_id")]
        public int Product_Id { get; set; }
        [JsonProperty(PropertyName ="product_name")]
        public string Product_Name { get; set; }
        [JsonProperty(PropertyName ="product_net_price")]
        public decimal Product_Net_Price { get; set; }
        [JsonProperty(PropertyName ="product_gross_price")]
        public decimal Product_Gross_Price { get; set; }
        [JsonProperty(PropertyName ="club_id")]
        public List<int> Club_Id { get; set; }
        [JsonProperty(PropertyName ="product_quantity")]
        public int Product_Quantity { get; set; }
        [JsonProperty(PropertyName ="product_status")]
        public int Product_Status { get; set; }
        [JsonProperty(PropertyName ="category_id")]
        public List<int> Category_Id { get; set; }
    }
}
