using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Models
{
    public class CarDetailsViewModel
    {
        public int CarId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Registration Plate")]
        public string RegistrationPlate { get; set; }

        [Display(Name = "Brand")]
        public string BrandCarName { get; set; }

        [Display(Name = "Model")]
        public string ModelCarName { get; set; }
    }
}
