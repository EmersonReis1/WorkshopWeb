using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Data.Repositories
{
    public class ReparationRepository : GenericRepository<Reparation>, IReparationRepository
    {
        private readonly DataContext _context;
        private readonly ICarRepository _carRepository;
        private readonly IBrandCarRepository _brandCarRepository;

        public ReparationRepository(DataContext context, ICarRepository carRepository, IBrandCarRepository brandCarRepository) : base(context)
        {
            _context = context;
            _carRepository = carRepository;
            _brandCarRepository = brandCarRepository;
        }

        public async Task<bool> AddReparationsAsync(AppointmentService model)
        {
            if (model == null)
            {
                return false;
            }

            var car = new Car();

            if (model.User == null)
            {
                car = await _carRepository.GetCarWithNotUserAsync(model.UserNoRegistry);
            }
            else
            {
                car = await _carRepository.GetCarWithUserAsync(model.User, model.RegistrationPlate);
            }

            if (car == null)
            {
                return false;
            }

            var serviceDetail = new List<ServiceDetail>();

            foreach (var item in model.Services)
            {
                serviceDetail.Add(new ServiceDetail { Service = item.Service, State = false});
            }

            var brand = await _brandCarRepository.GetBrandCarAsync(model.ModelCar);

            model.State = "Concluded";

            var reparation = new Reparation
            {
                Car = car,
                ServiceDetails = serviceDetail,
                Appointment = model,
                State = "Pendant",
                DateCreate = DateTime.UtcNow,
                BrandCar = brand,
                
            };

            _context.AppointmentServices.Update(model);
            await _context.Reparations.AddAsync(reparation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Reparation> GetReparationByIdAsync(int id)
        {
            return await _context.Reparations
                                 .Include(s => s.ServiceDetails)
                                 .ThenInclude(e => e.Service)
                                 .Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ReparationViewModel> GetReparationViewModel(User user)
        {
            var all = await _context.Reparations
                           .Include(c => c.Car)
                           .ThenInclude(m => m.ModelCar)
                           .OrderByDescending(o => o.DateCreate).Where(n => n.State == "Pendant")
                           .Select(n => new ItemReparationViewModel
                           {
                               Id = n.Id,
                               Brand = n.BrandCar.Name,
                               Model = n.Car.ModelCar.Name,
                               RegistrationPlate = n.Car.RegistrationPlate,
                               Services = n.ServiceDetails.Count(),
                           })
                           .ToListAsync();

            var allUser = await _context.Reparations
                           .Include(c => c.Car)
                           .ThenInclude(m => m.ModelCar)
                           .OrderByDescending(o => o.DateCreate)
                           .Where(u => u.UserEmployee == user && u.State == "Execution")
                           .Select(n => new ItemReparationViewModel
                           {
                               Id = n.Id,
                               Brand = n.BrandCar.Name,
                               Model = n.Car.ModelCar.Name,
                               RegistrationPlate = n.Car.RegistrationPlate,
                               Services = n.ServiceDetails.Count(),
                           })
                           .ToListAsync();




            return new ReparationViewModel
            {
                 AllServices = all,
                 UserServices = allUser,
            };

        }

     
    }
}
