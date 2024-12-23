using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class RequesterService
    {
        private static readonly HttpClient client = new HttpClient();

        //PeformPost
        public async Task<string> PostRequestJsonAsync(string url, object data)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                throw new HttpRequestException("Request Failed. Status code was not 200");
            }
            catch (HttpRequestException httpEx)
            {
                return "";
            }
            catch (TaskCanceledException taskEx)
            {
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}