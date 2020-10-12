using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Data.Repositories;
using WorkshopWeb.Helpers;
using WorkshopWeb.Models;

namespace WorkshopWeb.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IBrandCarRepository _brandCarRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public CarsController(ICarRepository carRepository, IBrandCarRepository brandCarRepository, IUserHelper userHelper,
            IConverterHelper converterHelper)
        {
            _carRepository = carRepository;
            _brandCarRepository = brandCarRepository;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int? pageNumber)
        {

            var model = _carRepository.GetAllCar();

            if (model == null)
            {
                return NotFound();
            }

            int pageSize = 10;
            return View(await PaginatedHelper<Car>.CreateAsync(model, pageNumber ?? 1, pageSize));
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetCarAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            var Brand = await _brandCarRepository.GetBrandCarAsync(car.ModelCar);
            if (Brand == null)
            {
                return NotFound();
            }


            return View(CarToDetails(car, Brand));
        }

        private CarDetailViewModel CarToDetails(Car car, BrandCar brand)
        {
            return new CarDetailViewModel
            {
                Id = car.Id,
                BrandCar = brand,
                ModelCar = car.ModelCar,
                RegistrationPlate = car.RegistrationPlate,
                User = car.User,
                UserNoRegistry = car.UserNoRegistry,

            };
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new CarViewModel
            {
                Brands = _brandCarRepository.GetComboBrandCars(),
                ModelCars = _brandCarRepository.GetComboModelCars(0),
            };

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }
                var modelcar = await _brandCarRepository.GetModelCarAsync(model.ModelCarId);
                if (modelcar == null)
                {
                    return NotFound();
                }

               
                await _carRepository.CreateAsync(_converterHelper.ToCar(model, user, modelcar, true));
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            var model = new CarViewModel
            {
                Brands = _brandCarRepository.GetComboBrandCars(),
                ModelCars = _brandCarRepository.GetComboModelCars(0),
                
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carRepository.UpdateAsync(car);
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _carRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            await _carRepository.DeleteAsync(brand);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetModelCarsAsync(int brandId)
        {
            var brand = await _brandCarRepository.GetBrandCarsWithModeCarsAsync(brandId);
            return this.Json(brand.Model.OrderBy(c => c.Name));
        }
    }
}
