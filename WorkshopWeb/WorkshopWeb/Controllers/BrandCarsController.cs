using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkshopWeb.Data;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Data.Repositories;
using WorkshopWeb.Models;

namespace WorkshopWeb.Controllers
{
    public class BrandCarsController : Controller
    {
        private readonly IBrandCarRepository _brandCarRepository;

        public BrandCarsController(IBrandCarRepository brandCarRepository)
        {
            _brandCarRepository = brandCarRepository;
        }

        public async Task<IActionResult> DeleteModelCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelCar = await _brandCarRepository.GetModelCarAsync(id.Value);
            if (modelCar == null)
            {
                return NotFound();
            }

            var brandId = await _brandCarRepository.DeleteModelCarAsync(modelCar);
            return this.RedirectToAction($"Details/{brandId}");
        }


        public async Task<IActionResult> EditModelCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelCar = await _brandCarRepository.GetModelCarAsync(id.Value);
            if (modelCar == null)
            {
                return NotFound();
            }

            return View(modelCar);
        }


        [HttpPost]
        public async Task<IActionResult> EditModelCar(ModelCar modelCar)
        {
            if (this.ModelState.IsValid)
            {
                var brandId = await _brandCarRepository.UpdateModelCarAsync(modelCar);
                if (brandId != 0)
                {
                    return this.RedirectToAction($"Details/{brandId}");
                }
            }

            return this.View(modelCar);
        }



        public async Task<IActionResult> AddModelCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandCarRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            var model = new ModelCarViewModel { BrandCarId = brand.Id };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddModelCar(ModelCarViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _brandCarRepository.AddModelCarAsync(model);
                return this.RedirectToAction($"Details/{model.BrandCarId}");
            }

            return this.View(model);
        }


        public IActionResult Index()
        {
            return View(_brandCarRepository.GetBrandCarsWithModelCars());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandCarRepository.GetBrandCarsWithModeCarsAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }


        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCar brandCar)
        {
            if (ModelState.IsValid)
            {
                await _brandCarRepository.CreateAsync(brandCar);
                return RedirectToAction(nameof(Index));
            }

            return View(brandCar);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandCarRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandCar brandCar)
        {
            if (ModelState.IsValid)
            {
                await _brandCarRepository.UpdateAsync(brandCar);
                return RedirectToAction(nameof(Index));
            }

            return View(brandCar);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandCarRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            await _brandCarRepository.DeleteAsync(brand);
            return RedirectToAction(nameof(Index));
        }
    }
}
