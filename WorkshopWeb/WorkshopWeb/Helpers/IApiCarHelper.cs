using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities.ApiCar;

namespace WorkshopWeb.Helpers
{
    public interface IApiCarHelper
    {
        Task<IEnumerable<BrandApi>> GetBrandsApiAsync();

        Task<IEnumerable<ModelCarApi>> GetModelsApiAsync(int id);
    }
}
