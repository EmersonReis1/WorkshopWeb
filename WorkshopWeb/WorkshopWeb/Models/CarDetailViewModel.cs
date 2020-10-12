using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Models
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }

        public BrandCar BrandCar { get; set; }

        public ModelCar ModelCar { get; set; }

        [Display(Name = "Registration Plate")]
        public string RegistrationPlate { get; set; }

        public User User { get; set; }

        public UserNoRegistry UserNoRegistry { get; set; }
    }
}
