using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Data.Repositories
{
    public interface IBrandCarRepository : IGenericRepository<BrandCar>
    {


		IQueryable GetBrandCarsWithModelCars();


		Task<BrandCar> GetBrandCarsWithModeCarsAsync(int id);


		Task<ModelCar> GetModelCarAsync(int id);


		Task AddModelCarAsync(ModelCarViewModel model);


		Task<int> UpdateModelCarAsync(ModelCar modelCar);


		Task<int> DeleteModelCarAsync(ModelCar modelCar);


		IEnumerable<SelectListItem> GetComboBrandCars();


		IEnumerable<SelectListItem> GetComboModelCars(int brandCarId);


		Task<BrandCar> GetBrandCarAsync(ModelCar modelCar);

		BrandCar GetBrandCar(ModelCar modelCar);
	}
}
