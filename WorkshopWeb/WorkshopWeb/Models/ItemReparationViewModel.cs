using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Models
{
    public class ItemReparationViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string RegistrationPlate { get; set; }

        public int Services { get; set; }
    }
}
