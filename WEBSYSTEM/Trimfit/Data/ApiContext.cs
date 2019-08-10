using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trimfit.Data
{
    public class ApiContext
    {
        private static string BaseUrl = "http://api.trimfit.pl/api/";

        public ApiContext()
        {

        }
        public async Task<JsonResult> GetRequest(string url)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                using (var r = await client.GetAsync(url))
                {
                    if (r.IsSuccessStatusCode)
                    {
                        result = await r.Content.ReadAsStringAsync();
                    }
                    else
                        result = r.Content.ReadAsStringAsync().Result;
                    // powyższe odpowiedź i status cody do dopracowania
                }
            }
            return new JsonResult(result);
        }
        public async Task<JsonResult> PutRequest(string url, object model)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                using (var r = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(model, Formatting.Indented), Encoding.UTF8, "application/json")))
                {
                    result = r.StatusCode.ToString();
                }
            }

            return new JsonResult(result);
        }
        public async Task<JsonResult> PostRequest(string url, object model)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                using (var r = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model, Formatting.Indented), Encoding.UTF8, "application/json")))
                {
                    if (r.IsSuccessStatusCode)
                    {
                        result = r.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        result = r.Content.ReadAsStringAsync().Result;
                    }

                }
            }

            return new JsonResult(result);
        }
        public async Task<JsonResult> DeleteRequest(string url)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                using (var r = await client.DeleteAsync(url))
                {
                    if (r.IsSuccessStatusCode)
                    {
                        result = "200";
                    }
                    else
                    {
                        result = r.Content.ReadAsStringAsync().Result;
                    }

                }
            }
            return new JsonResult(result);
        }

    }
}
