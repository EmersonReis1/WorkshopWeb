using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Models
{
    public class AddAppointmentViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string Time { get; set; }

        //car

        //public string YearCar { get; set; }

        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a brand")]
        public int BrandId { get; set; }


        public IEnumerable<SelectListItem> Brands { get; set; }



        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a model")]
        public int ModelCarId { get; set; }


        public IEnumerable<SelectListItem> ModelCars { get; set; }

        [RegularExpression(@"^(([A-Z]{2}-\d{2}-(\d{2}|[A-Z]{2}))|(\d{2}-(\d{2}-[A-Z]{2}|[A-Z]{2}-\d{2})))$",
            ErrorMessage = "Example __-__-__")]
        [Display(Name = "Registration Plate")]
        [Required]
        public string RegistrationPlate { get; set; }

        //service

        public IList<SelectListItem> Services { get; set; }

        //workshop

        [Display(Name = "Workshop")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Workshop")]
        public int WorkshopId { get; set; }


        public IEnumerable<SelectListItem> Workshops { get; set; }

        public string UserId { get; set; }
    }
}
