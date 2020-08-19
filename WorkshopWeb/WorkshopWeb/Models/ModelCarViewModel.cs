using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Models
{
    public class ModelCarViewModel
    {
        public int BrandCarId { get; set; }

        public int ModelCarId { get; set; }

        [Required]
        [Display(Name = "Model")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Name { get; set; }

    }
}
