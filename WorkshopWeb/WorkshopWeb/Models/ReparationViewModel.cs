using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Models
{
    public class ReparationViewModel
    {

        public IEnumerable<ItemReparationViewModel> AllServices { get; set; }

        public IEnumerable<ItemReparationViewModel> UserServices { get; set; }
    }
}
