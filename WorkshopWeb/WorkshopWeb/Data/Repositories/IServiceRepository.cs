using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
 
        IList<SelectListItem> GetComboService();

        MultiSelectList GetMultiService();

        // Task<ICollection<Service>> GetListServiceIdsAsync(ICollection<int> ids);

    }
}
