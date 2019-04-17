using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class ProductController : Controller
    {
        // 1 - produkt
        // 2 - usługa
        public async Task<List<ProductView>> GetProductsList()
        {
            List<ProductView> products_view = new List<ProductView>();

            ApiContext _context = new ApiContext();

            var categories_response = await _context.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            var products_response = await _context.GetRequest("Products/");
            var products_list = JsonConvert.DeserializeObject<List<Product>>(products_response.Value.ToString());

            var club = await _context.GetRequest("Clubs/");
            var clubs_list = JsonConvert.DeserializeObject<List<Club>>(club.Value.ToString());

            foreach (var product in products_list)
            {
                var categories = categories_list.Where(c => product.Category_Id.Contains(c.Category_Id)).ToList();
                var clubs = clubs_list.Where(c => product.Club_Id.Contains(c.Club_Id)).ToList();

                products_view.Add(new ProductView()
                {
                    Club = clubs,
                    Product_Gross_Price = product.Product_Gross_Price,
                    Product_Name = product.Product_Name,
                    Product_Id = product.Product_Id,
                    Product_Net_Price = product.Product_Net_Price,
                    Product_Quantity = product.Product_Quantity,
                    Product_Status = product.Product_Status,
                    Product_Icon = product.Product_Icon,
                    Category = categories
                });

            }

            return products_view;
        }
        [HttpPost]
        public async Task<JsonResult> PostProduct(int product_id, string product_name, decimal product_net_price, decimal product_gross_price, int product_quantity, string product_icon, int product_status, List<int> category_id, List<int> club_id)
        {
            ApiContext _context = new ApiContext();
            string result = "";

            var product = new Product()
            {
                Category_Id = category_id,
                Club_Id = club_id,
                Product_Gross_Price = product_gross_price,
                Product_Icon = product_icon,
                Product_Status = product_status,
                Product_Name = product_name,
                Product_Net_Price = product_net_price,
                Product_Quantity = product_quantity
            };

            try
            {
                JsonResult response = await _context.PostRequest("Products/", product);
                result = response.Value.ToString();
                // zastanowić się w jaki sposób czytać i przekazywać responsy!!!
            }
            catch (HttpRequestException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }
        [HttpPost]
        public JsonResult AddToCart(int product_id)
        {
            try
            {
                if (Request.Cookies["products_cart"] != null)
                {
                    List<string> new_elements_list = new List<string>();
                    bool checker = false;
                    var elements = Request.Cookies["products_cart"].Split(",");
                    foreach (var element in elements)
                    {
                        var single_element = element.Split(":");
                        if (single_element[0] == product_id.ToString())
                        {
                            checker = true;
                            var q = int.Parse(single_element[1]);
                            q += 1;
                            var new_element = single_element[0] + ":" + q.ToString();
                            new_elements_list.Add(new_element);
                        }
                        else
                            new_elements_list.Add(element);

                    }
                    if (checker == false)
                        new_elements_list.Add(product_id.ToString() + ":1");

                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(15);
                    Response.Cookies.Append("products_cart", String.Join(",", new_elements_list), option);
                }
                else
                {
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(15);
                    Response.Cookies.Append("products_cart", product_id.ToString() + ":1", option);
                }


            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult("OK");
        }
        public IActionResult Index()
        {
            // TODO: coś z tym zrobić
            return View();
        }
        public async Task<IActionResult> List()
        {
            var products_list = await this.GetProductsList();

            ViewData["header"] = "Wszystkie produkty";
            return View(products_list);
        }
        public async Task<IActionResult> Manage()
        {
            var products_list = await this.GetProductsList();

            ViewData["header"] = "Zarządzaj produktami";
            return View(products_list);
        }
        public IActionResult Add()
        {
            ViewData["header"] = "Dodaj nowy produkt";
            return View();
        }
    }
}