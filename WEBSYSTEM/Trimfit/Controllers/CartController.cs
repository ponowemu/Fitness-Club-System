using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

            string[] categories = new string[] { "products_cart", "vouchers_cart", "services_cart" };

            foreach (var cat in categories)
            {
                if (Request.Cookies[cat] != null && Request.Cookies[cat] != "")
                {
                    var elements = Request.Cookies[cat].Split(",");
                    foreach (var element in elements)
                    {
                        CartView cart_element = null;

                        if (cat == "products_cart")
                        {
                            var product_response = await _context.GetRequest("Products/" + element.Split(":")[0] + "");
                            var el = JsonConvert.DeserializeObject<Product>(product_response.Value.ToString());

                            var quantity = Convert.ToInt16(element.Split(":")[1]);
                            cart_element = new CartView()
                            {
                                Element_Additional_Info = "",
                                Element_Gross_Price = el.Product_Gross_Price,
                                Element_Id = el.Product_Id,
                                Element_Name = el.Product_Name,
                                Element_Net_Price = el.Product_Net_Price,
                                Element_Type = "1",
                                Element_Quantity = quantity,
                                Element_Total = el.Product_Gross_Price * quantity
                            };
                        }
                        else if (cat == "vouchers_cart")
                        {
                            var product_response = await _context.GetRequest("Vouchers/" + element.Split(":")[0] + "");
                            var el = JsonConvert.DeserializeObject<Voucher>(product_response.Value.ToString());

                            var quantity = Convert.ToInt16(element.Split(":")[1]);
                            cart_element = new CartView()
                            {
                                Element_Additional_Info = "",
                                Element_Gross_Price = el.Voucher_Gross_Price,
                                Element_Id = el.Voucher_Id.Value,
                                Element_Name = el.VoucherType.Voucher_Type_Name,
                                Element_Net_Price = el.Voucher_Net_Price,
                                Element_Type = "2",
                                Element_Quantity = quantity,
                                Element_Total = el.Voucher_Gross_Price * quantity
                            };
                        }
                        else if (cat == "services_cart")
                        {
                            var product_response = await _context.GetRequest("Services/" + element.Split(":")[0] + "");
                            var el = JsonConvert.DeserializeObject<Service>(product_response.Value.ToString());

                            var quantity = Convert.ToInt16(element.Split(":")[1]);
                            cart_element = new CartView()
                            {
                                Element_Additional_Info = "",
                                Element_Gross_Price = el.Service_Gross_Price,
                                Element_Id = el.Service_Id,
                                Element_Name = el.Service_Name,
                                Element_Net_Price = el.Service_Net_Price,
                                Element_Type = "3",
                                Element_Quantity = quantity,
                                Element_Total = el.Service_Gross_Price * quantity
                            };
                        }

                        if (cart_element != null)
                            elements_list.Add(cart_element);
                    }
                }
            }

            return elements_list;
        }
        private void AddToCartInternal(string name, int id)
        {
            if (Request.Cookies[name] != null)
            {
                List<string> new_elements_list = new List<string>();
                bool checker = false;
                var elements = Request.Cookies[name].Split(",");
                foreach (var element in elements)
                {
                    var single_element = element.Split(":");
                    if (single_element[0] == id.ToString())
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
                    new_elements_list.Add(id.ToString() + ":1");

                var option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(15);
                Response.Cookies.Append(name, String.Join(",", new_elements_list), option);
            }
            else
            {
                var option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(15);
                Response.Cookies.Append(name, id.ToString() + ":1", option);
            }
        }
        [HttpPost]
        public JsonResult AddToCart(int? product_id, int? voucher_id, int? service_id)
        {
            try
            {
                if (product_id.HasValue)
                {
                    AddToCartInternal("products_cart", product_id.Value);
                }
                if (voucher_id.HasValue)
                {
                    AddToCartInternal("vouchers_cart", voucher_id.Value);
                }
                if (service_id.HasValue)
                {
                    AddToCartInternal("services_cart", service_id.Value);
                }
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult("OK");
        }
        [HttpPost]
        public JsonResult RemoveFromCart(int product_id)
        {
            try
            {
                if (Request.Cookies["products_cart"] != null)
                {
                    int i = 0;
                    List<string> new_elements_list = new List<string>();
                    var elements = Request.Cookies["products_cart"].Split(",");
                    foreach (var element in elements)
                    {
                        var single_element = element.Split(":");
                        if (single_element[0] != product_id.ToString())
                            new_elements_list.Add(element);
                        i++;
                    }

                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(15);
                    if (i == 1)
                        Response.Cookies.Append("products_cart", null, option);
                    else
                        Response.Cookies.Append("products_cart", String.Join(",", new_elements_list), option);
                }

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult("");
        }
        [HttpPost]
        public JsonResult ChangeQuantityFromCart(int product_id, int quantity)
        {
            try
            {
                if (Request.Cookies["products_cart"] != null)
                {
                    List<string> new_elements_list = new List<string>();
                    var elements = Request.Cookies["products_cart"].Split(",");
                    foreach (var element in elements)
                    {
                        var single_element = element.Split(":");
                        if (single_element[0] != product_id.ToString())
                            new_elements_list.Add(element);
                        else
                        {
                            int qua;
                            var pid = single_element[0];

                            qua = quantity;

                            var nel = single_element[0] + ":" + qua.ToString();
                            new_elements_list.Add(nel);
                        }

                    }

                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(15);
                    Response.Cookies.Append("products_cart", String.Join(",", new_elements_list), option);
                }

            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult("");
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = "Koszyk zakupów";
            var result = await this.GetElements();
            return View(result);
        }

    }
}