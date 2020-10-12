using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Data.Repositories;
using WorkshopWeb.Helpers;
using WorkshopWeb.Models;

namespace WorkshopWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IAppointmentServiceRepository _AppointmentServiceRepository;
        private readonly IAddressWorkshopRepository _addressWorkshopRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBrandCarRepository _brandCarRepository;
        private readonly IReparationRepository _reparationRepository;
        private readonly ICarRepository _carRepository;

        public AppointmentsController(IServiceRepository serviceRepository, IAppointmentServiceRepository AppointmentServiceRepository,
                                    IAddressWorkshopRepository addressWorkshopRepository, IUserHelper userHelper,
                                    IBrandCarRepository brandCarRepository, IReparationRepository reparationRepository,
                                    ICarRepository carRepository)
        {
            _serviceRepository = serviceRepository;
            _AppointmentServiceRepository = AppointmentServiceRepository;
            _addressWorkshopRepository = addressWorkshopRepository;
            _userHelper = userHelper;
            _brandCarRepository = brandCarRepository;
            _reparationRepository = reparationRepository;
            _carRepository = carRepository;
        }


        public async Task<IActionResult> Create()
        {

            var userName = this.User.Identity.Name;
            if (userName != null)
            {
                var user = await _userHelper.GetUserByEmailAsync(userName);
                if (user != null)
                {
                    var modelUser = new AddAppointmentViewModel
                    {
                        Brands = _brandCarRepository.GetComboBrandCars(),
                        ModelCars = _brandCarRepository.GetComboModelCars(0),
                        Services = _serviceRepository.GetComboService(),
                        Workshops = _addressWorkshopRepository.GetComboServiceAddressWorkshop(),
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        UserId = user.Id,
                    };
                    return View(modelUser);
                }

            }

            var model = new AddAppointmentViewModel
            {
                Brands = _brandCarRepository.GetComboBrandCars(),
                ModelCars = _brandCarRepository.GetComboModelCars(0),
                Services = _serviceRepository.GetComboService(),
                Workshops = _addressWorkshopRepository.GetComboServiceAddressWorkshop()

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                await _AppointmentServiceRepository.AddAppointmentAsync(model, user);

                return this.RedirectToAction("InfoAppointment");
            }

            return View(model);
        }


        public async Task<JsonResult> GetModelCarsAsync(int brandId)
        {
            var brand = await _brandCarRepository.GetBrandCarsWithModeCarsAsync(brandId);
            return this.Json(brand.Model.OrderBy(c => c.Name));
        }


        [Authorize]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Recessionist"))
            {
                var model = await _AppointmentServiceRepository.GetAppointmentAsync(this.User.Identity.Name);

                if (model == null)
                {
                    return NotFound();
                }

                int pageSize = 10;
                return View(await PaginatedHelper<AppointmentService>.CreateAsync(model.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

            return RedirectToAction("NotAuthorized", "Account");
        }


        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Recessionist"))
            {
                if (!id.HasValue)
                {
                    return NotFound();
                }

                var model = _AppointmentServiceRepository.GetAppointmentById(id.Value).FirstOrDefault();

                if (model == null)
                {
                    return NotFound();
                }

                var brand = await _brandCarRepository.GetBrandCarAsync(model.ModelCar);
                if (brand != null)
                {
                    string brandModel = $"{brand.Name} {model.ModelCar.Name}";
                    model.ModelCar.Name = brandModel;
                }

                return View(model);

            }

            return RedirectToAction("NotAuthorized", "Account");

        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Recessionist"))
            {
                await _AppointmentServiceRepository.DeleteAppointmentAsync(id.Value);
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult InfoAppointment()
        {
            return View();
        }

         

        [Authorize]
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
               return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
              return  NotFound();

            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Recessionist"))
            {
                var Appointment = await _AppointmentServiceRepository.GetAppointmentById(id.Value).FirstOrDefaultAsync();
                if (Appointment == null)
                {
                   return NotFound();
                }

                await _reparationRepository.AddReparationsAsync(Appointment);
                Appointment.State = "Conclude";
                await _AppointmentServiceRepository.UpdateAsync(Appointment);
               return RedirectToAction(nameof(Index));
            }

           return RedirectToAction("NotAuthorized", "Account");
        }


    }
}

