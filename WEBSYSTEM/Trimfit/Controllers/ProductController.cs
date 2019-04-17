using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;
using Trimfit.Models.ModelView;

namespace Trimfit.Controllers
{
    public class ProductController : Controller
    {
        public async Task<ProductView> GetProductsList()
        {
            List<ProductView> products_view = new List<ProductView>();

            ApiContext _context = new ApiContext();

            var categories_response = await _context.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var products_response  = await _context.GetRequest("Products/");
            var products_list = JsonConvert.DeserializeObject<List<Product>>(products_response.Value.ToString());

            var club = await _context.GetRequest("Clubs/");
            var clubs_list = JsonConvert.DeserializeObject<List<Club>>(club.Value.ToString());

            foreach (var product in products_list)
            {
                var categories = categories_list.Where(c => product.Category_Id.Contains(c.Category_Id)).ToList();
                var clubs = clubs_list.Where(c => product.Club_Id.Contains(c.Club_Id)).ToList();

                products_view.Add(new ProductView() {
                    Club = clubs,
                    Product_Gross_Price = product.Product_Gross_Price,
                    Product_Name = product.Product_Name,
                    Product_Id = product.Product_Id,
                    Product_Net_Price = product.Product_Net_Price,
                    Product_Quantity = product.Product_Quantity,
                    Product_Status = product.Product_Status
                });

            }

            return products_view;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}