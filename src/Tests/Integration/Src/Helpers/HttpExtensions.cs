using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Integration.Helpers
{
    internal static class HttpExtensions
    {
        public static async Task<HttpResponseMessage> PostDataAsync(this HttpClient client, string url, Dictionary<string, string> data)
        {
            var obj = JsonConvert.SerializeObject(data);

            var response = await client.PostAsync(url, new StringContent(obj, Encoding.UTF8, "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> PatchDataAsync(this HttpClient client, string url, Dictionary<string, string> data)
        {
            var obj = JsonConvert.SerializeObject(data);

            var response = await client.PatchAsync(url, new StringContent(obj, Encoding.UTF8, "application/json"));

            return response;
        }

        public static async Task<ulong> PostDefaultTaskAsync(this HttpClient client)
        {
            var data = new Dictionary<string, string>
            {
                {"title", "Tests task"},
                {"description", "My task for research"},
                {"ExpirationUtc", $"{DateTime.UtcNow.AddHours(5)}"}
            };

            var response = await client.PostDataAsync("tasks", data);

            var responseData = await response.Content.ReadAsStringAsync();

            var id = ResponseHelper.GetDataFromResponse<ulong>(responseData, "id");

            return id;
        }
    }
}
