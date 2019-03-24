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
        public HttpClient Client { get; private set; }
        private static string BaseUrl = "http://api.trimfit.pl/api/";

        public ApiContext()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
        }
        public async Task<JsonResult> GetRequest(string url)
        {
            string result = "";
            using (var client = Client)
            {
                using (var r = await client.GetAsync(url))
                {
                    if (r.StatusCode.Equals(200))
                        result = await r.Content.ReadAsStringAsync();
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
            using (var client = Client)
            {
                using (var r = await client.PutAsJsonAsync(url, JsonConvert.SerializeObject(model)))
                {
                    result = r.StatusCode.ToString();
                }
            }

            return new JsonResult(result);
        }
        public async Task<JsonResult> PostRequest(string url, object model)
        {
            string result = "";
            using (var client = Client)
            {
                using (var r = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model, Formatting.Indented), Encoding.UTF8, "application/json")))
                {
                    result = r.StatusCode.ToString();
                }
            }

            return new JsonResult(result);
        }
        public HttpResponseMessage DeleteRequest(string url)
        {
            return Client.DeleteAsync(url).Result;
        }

    }
}
