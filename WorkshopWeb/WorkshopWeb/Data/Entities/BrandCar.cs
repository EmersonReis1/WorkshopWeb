using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Data.Entities
{
    public class BrandCar : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        [Required]
        [Display(Name = "Brand")]
        public string Name { get; set; }

        public ICollection<ModelCar> Model { get; set; }

        [Display(Name = "# Models")]
        public int NumberModels { get { return this.Model == null ? 0 : this.Model.Count; } }

    }
}
