using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data
{
    
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<BrandCar> BrandCars { get; set; }

        public DbSet<ModelCar> ModelCars { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<AddressWorkshop> AddressWorkshops { get; set; }

        public DbSet<AppointmentService> AppointmentServices { get; set; }

        public DbSet<AppointmentAndService> AppointmentAndServices { get; set; }
        
        public DbSet<UserNoRegistry> UserNoRegistrys { get; set; }

        public DbSet<Reparation> Reparations { get; set; }

        public DbSet<ServiceDetail> ServiceDetails { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
