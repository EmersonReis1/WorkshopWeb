using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Models
{
    public class RegisterNewEmployeesViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "NIF")]
        public string Nif { get; set; }

        [Required(ErrorMessage = "You must select a Roles")]
        [Display(Name = "Roles")]
        public string RoleName { get; set; }


        public IEnumerable<SelectListItem> Roles { get; set; }

        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a city")]
        public int CityId { get; set; }


        public IEnumerable<SelectListItem> Cities { get; set; }



        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country")]
        public int CountryId { get; set; }


        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
