using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Models
{
    public class ConfirnReparationViewModel
    {
        public CarViewModel CarViewModel { get; set; }
         

        public AppointmentService Schedule { get; set; }
    }
}
