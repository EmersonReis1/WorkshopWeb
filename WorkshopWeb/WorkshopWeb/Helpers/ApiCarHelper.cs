using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities.ApiCar;

namespace WorkshopWeb.Helpers
{
    public class ApiCarHelper : IApiCarHelper
    {
        private readonly IConfiguration _configuration;

        public ApiCarHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<BrandApi>> GetBrandsApiAsync()
        {
            var controler = _configuration["ApiCar:AllBrand"];

            return JsonConvert.DeserializeObject<List<BrandApi>>(await ApiConnect(controler));

        }

        public async Task<IEnumerable<ModelCarApi>> GetModelsApiAsync(int id)
        {
            var controler = _configuration["ApiCar:AllModelId"];

            var result = new List<ModelCarApi>();
            try
            {
                result = JsonConvert.DeserializeObject<List<ModelCarApi>>(await ApiConnect($"{controler}{id.ToString()}.json"));
            }
            catch (Exception)
            {

                result = null;
            }
            
            return result;

        }

        private async Task<string> ApiConnect(string controller)
        {
            var urlBase = _configuration["ApiCar:UrlBase"];

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var response = await client.GetAsync(controller);

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception)
            {
                return null;
            }
        } 
    }
}
