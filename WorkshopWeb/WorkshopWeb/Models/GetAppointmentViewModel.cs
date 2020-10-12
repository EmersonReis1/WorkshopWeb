using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Models
{
    public class GetAppointmentViewModel
    {

       
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Nunber")]
        public string PhoneNunber { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public int QuantityServices { get; set; }

        [DisplayName("Order date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }

       
        public string Workshop { get; set; }

    }
}
