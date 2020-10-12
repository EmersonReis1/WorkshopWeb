using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IList<SelectListItem> GetComboService()
        {
            var list = _context.Services.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
                Selected = false,
                 
            }).ToList();

            return list;
        }

        public MultiSelectList GetMultiService()
        {
            MultiSelectList listItems = new MultiSelectList(GetComboService().OrderBy(i => i.Text), "Value", "Text");

            return listItems;
        }
    }
}
