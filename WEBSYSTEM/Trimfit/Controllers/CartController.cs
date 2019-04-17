using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class CartController : Controller
    {
        public async Task<List<CartView>> GetElements()
        {
            List<CartView> elements_list = new List<CartView>();
            ApiContext _context = new ApiContext();

            if (Request.Cookies["products_cart"] != null)
            {
                var elements = Request.Cookies["products_cart"].Split(",");
                foreach(var element in elements)
                {
                    var product_response = await _context.GetRequest("Products/"+ element.Split(":")[0] +"");
                    var product = JsonConvert.DeserializeObject<Product>(product_response.Value.ToString());
                    var quantity = Convert.ToInt16(element.Split(":")[1]);
                    var cart_element = new CartView()
                    {
                        Element_Additional_Info = "",
                        Element_Gross_Price = product.Product_Gross_Price,
                        Element_Id = product.Product_Id,
                        Element_Name = product.Product_Name,
                        Element_Net_Price = product.Product_Net_Price,
                        Element_Type = "1",
                        Element_Quantity = quantity,
                        Element_Total = product.Product_Gross_Price * quantity
                    };
                    elements_list.Add(cart_element);
                }
            }
            return elements_list;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = "Koszyk zakupów";
            var result = await this.GetElements();   
            return View(result);
        }
    }
}