using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data.Repositories
{
    public class AddressWorkshopRepository : GenericRepository<AddressWorkshop>, IAddressWorkshopRepository
    {
        private readonly DataContext _context;

        public AddressWorkshopRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboServiceAddressWorkshop()
        {
            var list = _context.AddressWorkshops.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Workshop)",
                Value = "0"
            });

            return list;
        }
    }
}
