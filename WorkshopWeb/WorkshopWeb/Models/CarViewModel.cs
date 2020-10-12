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
        public int Id { get; set; }

        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Brand")]
        public int BrandId { get; set; }


        public IEnumerable<SelectListItem> Brands { get; set; }


        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model")]
        public int ModelCarId { get; set; }


        public IEnumerable<SelectListItem> ModelCars { get; set; }


        [RegularExpression(@"^(([A-Z]{2}-\d{2}-(\d{2}|[A-Z]{2}))|(\d{2}-(\d{2}-[A-Z]{2}|[A-Z]{2}-\d{2})))$",
            ErrorMessage ="Example __-__-__")]
        [Display(Name = "Registration Plate")]
        [Required]
        public string RegistrationPlate { get; set; }
    }
}
