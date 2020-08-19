using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Data.Entities
{
    public class Car : IEntity
    {
        public int Id { get; set; }

     
        public int ModelCarId { get; set; }

        public ModelCar ModelCar { get; set; }

        [Display(Name = "Registration Plate")]
        [Required]
        public string RegistrationPlate { get; set; }

        public User User { get; set; }


    }
}
