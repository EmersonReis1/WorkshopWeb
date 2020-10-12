using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Helpers;
using WorkshopWeb.Models;

namespace WorkshopWeb.Data.Repositories
{

    public class AppointmentServiceRepository : GenericRepository<AppointmentService>, IAppointmentServiceRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICarRepository _carRepository;
        private readonly IBrandCarRepository _brandCarRepository;

        public AppointmentServiceRepository(DataContext context, IUserHelper userHelper, ICarRepository carRepository, IBrandCarRepository brandCarRepository) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
            _carRepository = carRepository;
            _brandCarRepository = brandCarRepository;
        }

        public async Task<bool> AddAppointmentAsync(AddAppointmentViewModel model, User user)
        {


            if (model.Services == null || model.Services.Count == 0)
            {
                return false;
            }

            var itemService = model.Services.Where(s => s.Selected == true).ToList();



            var service = new List<AppointmentAndService>();

            foreach (var item in itemService)
            {
                var result = _context.Services.Where(sc => sc.Id == Convert.ToInt32(item.Value))
                .Select(s => new AppointmentAndService
                {
                    Service = s,
                });

                service.Add(result.FirstOrDefault());
            }

            var address = await _context.AddressWorkshops.FindAsync(model.WorkshopId);
            var modelCar = await _context.ModelCars.FindAsync(model.ModelCarId);


            if (user == null)
            {
                var noUser = new UserNoRegistry
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,

                };



                var newCar = new Car
                {
                    ModelCar = modelCar,
                    RegistrationPlate = model.RegistrationPlate,
                    UserNoRegistry = noUser,
                };

                await _carRepository.CreateAsync(newCar);


                var Appointment = new AppointmentService
                {
                    AddressWorkshop = address,
                    ModelCar = modelCar,
                    CreateDate = DateTime.UtcNow,
                    DeliveryDate = Convert.ToDateTime($"{model.Date},{model.Time}"),
                    User = null,
                    Services = service,
                    State = "Pendant",
                    UserNoRegistry = noUser,
                    RegistrationPlate = model.RegistrationPlate,

                };

                await _context.AppointmentServices.AddAsync(Appointment);
            }
            else
            {
                var car = await _carRepository.GetCarWithUserAsync(user ,model.RegistrationPlate);

                if (car == null)
                {

                    var newCar = new Car
                    {
                        ModelCar = modelCar,
                        RegistrationPlate = model.RegistrationPlate,
                        User = user,
                    };

                    await _carRepository.CreateAsync(newCar);
                }

                var schedule = new AppointmentService
                {
                    AddressWorkshop = address,
                    ModelCar = modelCar,
                    CreateDate = DateTime.UtcNow,
                    DeliveryDate = Convert.ToDateTime($"{model.Date},{model.Time}"),
                    User = user,
                    Services = service,
                    State = "Pendant",
                    UserNoRegistry = null,
                    RegistrationPlate = model.RegistrationPlate,
                };

                await _context.AppointmentServices.AddAsync(schedule);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var Appointment = await _context.AppointmentServices.Include(s => s.Services)
                .Where(sc => sc.Id == id).FirstOrDefaultAsync();
            if (Appointment == null)
            {
                return false;
            }

            _context.AppointmentAndServices.RemoveRange(Appointment.Services);
            _context.AppointmentServices.Remove(Appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IQueryable<AppointmentService>> GetAppointmentAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Recessionist"))
            {
                return _context.AppointmentServices
                    .Include(u => u.User)
                    .Include(s => s.Services)
                    .Include(a => a.AddressWorkshop)
                    .ThenInclude(c => c.City)
                    .Include(nu => nu.UserNoRegistry)
                    .Include(m => m.ModelCar)
                    .Where(n => n.State != "Conclude")
                    .OrderByDescending(sc => sc.CreateDate);
            }

            return _context.AppointmentServices
                .Include(o => o.Services)
                .Include(n => n.AddressWorkshop)
                .ThenInclude(n => n.City)
                .Include(n => n.ModelCar)
                .Include(s => s.User)
                .Where(us => us.User == user)
                .OrderByDescending(sc => sc.CreateDate);

        }

        public IQueryable<AppointmentService> GetAppointmentById(int id)
        {
            return _context.AppointmentServices
                    .Include(u => u.User)
                    .Include(s => s.Services)
                    .ThenInclude(sc => sc.Service)
                    .Include(a => a.AddressWorkshop)
                    .ThenInclude(c => c.City)
                    .Include(nu => nu.UserNoRegistry)
                    .Include(m => m.ModelCar)
                    .Where(o => o.Id == id)
                    .OrderByDescending(sc => sc.CreateDate);
        }
    }
}
