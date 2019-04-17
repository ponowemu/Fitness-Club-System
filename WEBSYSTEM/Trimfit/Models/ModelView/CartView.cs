using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class CartView
    {
        public int Element_Id { get; set; }
        public string Element_Name { get; set; }
        public decimal Element_Net_Price { get; set; }
        public decimal Element_Gross_Price { get; set; }
        public int Element_Quantity { get; set; }
        public string Element_Additional_Info { get; set; }
        public string Element_Type { get; set; }
        public decimal Element_Total { get; set; }
    }
}
