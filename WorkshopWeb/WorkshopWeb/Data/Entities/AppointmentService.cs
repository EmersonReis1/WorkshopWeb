using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Data.Entities
{
    public class AppointmentService : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Workshop")]
        public AddressWorkshop AddressWorkshop { get; set; }

        [Required]
        public ModelCar ModelCar { get; set; }


        [Required]
        public IEnumerable<AppointmentAndService> Services { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Services")]
        public int QuantityServices { get { return this.Services == null ? 0 : this.Services.Count();} }

        public User User { get; set; }

        [DisplayName("Delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }


        [DisplayName("Creation date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime CreateDate { get; set; }

        public string RegistrationPlate { get; set; }

        public UserNoRegistry  UserNoRegistry{ get; set; }


        public string State { get; set; }


    }
}
