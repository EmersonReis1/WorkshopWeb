using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Models
{
    public class AddReparationViewModel
    {
        public Car Car { get; set; }

        public AppointmentService ScheduleService { get; set; }
    }
}
