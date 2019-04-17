using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models.ModelView
{
    public class ProductView
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Net_Price { get; set; }
        public decimal Product_Gross_Price { get; set; }
        public List<Club> Club { get; set; }
        public int Product_Quantity { get; set; }
        public int Product_Status { get; set; }
        public List<Category> Category { get; set; }
    }
}
