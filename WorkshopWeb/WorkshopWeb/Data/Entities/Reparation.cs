using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Data.Entities
{
    public class Reparation : IEntity
    {
        public int Id { get; set; }

        
        public Car Car { get; set; }

        public BrandCar BrandCar { get; set; }

        public User UserEmployee { get; set; }

        
        public AppointmentService Appointment { get; set; }

        public DateTime DateCreate { get; set; }

        public string State { get; set; }

        public string Observation { get; set; }

        public IList<ServiceDetail> ServiceDetails { get; set; }

    }
}
