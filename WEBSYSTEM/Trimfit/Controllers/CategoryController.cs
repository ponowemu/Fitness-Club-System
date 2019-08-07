﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;

namespace Trimfit.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<List<Category>> GetAsync()
        {
            ApiContext _context = new ApiContext();
            List<Category> categories = new List<Category>();

            var categories_response = await _context.GetRequest("Categories/");
            var categories_list = JsonConvert.DeserializeObject<List<Category>>(categories_response.Value.ToString());

            foreach (var item in categories_list)
            {
                if (item.Category_Type == 1)
                    categories.Add(item);
            }

            return categories;
        }
        

        public async Task<JsonResult> AddAsync(string category_name, string category_color)
        {

            ApiContext _context = new ApiContext();
            Category category = new Category();
            string result = "";

            try
            {
                category.Category_Name = category_name;
                category.Category_Color = category_color;
                category.Category_Type = 1;

                JsonResult response = await _context.PostRequest("Categories/", category);
                result = response.Value.ToString();
                // zastanowić się w jaki sposób czytać i przekazywać responsy!!!
            }
            catch (HttpRequestException e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult("OK");
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            ViewData["Header"] = "Lista kategorii";

            var categories = await this.GetAsync();
            
            return View(categories);
        }
    }
}