using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Models
{
    public class CarViewModel
    {
        public int CarId { get; set; }

        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Brand")]
        public int BrandCarId { get; set; }


        public IEnumerable<SelectListItem> Brands { get; set; }


        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model")]
        public int ModelCarId { get; set; }


        public IEnumerable<SelectListItem> Models { get; set; }



        [Display(Name = "Registration Plate")]
        [Required]
        public string RegistrationPlate { get; set; }
    }
}
