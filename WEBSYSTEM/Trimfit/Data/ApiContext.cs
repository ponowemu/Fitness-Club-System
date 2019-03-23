using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Trimfit.Data
{
    public class ApiContext
    {
        public HttpClient Client { get; private set; }
        private static string BaseUrl = "http://api.trimfit.pl";

        public ApiContext()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
        }
        public async Task<JsonResult> GetResponse(string url)
        {
            string result = "";
            using (var client = Client)
            {
                using (var r = await client.GetAsync(new Uri(url)))
                {
                    if (r.StatusCode.Equals(200))
                        result = await r.Content.ReadAsStringAsync();
                    else
                        result = r.StatusCode.ToString();
                }
            }
            return new JsonResult(result);
        }
        public async Task<JsonResult> PutResponse(string url, object model)
        {
            string result = "";
            using (var client = Client)
            {
                using (var r = await client.PutAsJsonAsync(url, model))
                {
                    result = r.StatusCode.ToString();
                }
            }

            return new JsonResult(result);
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }

    }
}
