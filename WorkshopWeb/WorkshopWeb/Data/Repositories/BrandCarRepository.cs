using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Data.Repositories
{
    public class BrandCarRepository : GenericRepository<BrandCar>, IBrandCarRepository
    {
        private readonly DataContext _context;

        public BrandCarRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddModelCarAsync(ModelCarViewModel model)
        {
            var brand = await this.GetBrandCarsWithModeCarsAsync(model.BrandCarId);

            if (brand == null)
            {
                return;
            }

            brand.Model.Add(new ModelCar { Name = model.Name });
            _context.BrandCars.Update(brand);
            await _context.SaveChangesAsync();

        }

        public async Task<int> DeleteModelCarAsync(ModelCar modelCar)
        {
            var brand = await _context.BrandCars.Where(m => m.Model.Any(mo => mo.Id == modelCar.Id)).FirstOrDefaultAsync();
            if (brand == null)
            {
                return 0;
            }

            _context.ModelCars.Remove(modelCar);
            await _context.SaveChangesAsync();
            return brand.Id;

        }

        public async Task<BrandCar> GetBrandCarAsync(ModelCar modelCar)
        {
            return await _context.BrandCars.Where(b => b.Model.Any(m => m.Id == modelCar.Id)).FirstOrDefaultAsync();
        }

        public async Task<BrandCar> GetBrandCarsWithModeCarsAsync(int id)
        {
            return await _context.BrandCars.Include(m => m.Model).Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable GetBrandCarsWithModelCars()
        {
            return _context.BrandCars.Include(m => m.Model).OrderBy(m => m.Name);
        }

        public IEnumerable<SelectListItem> GetComboBrandCars()
        {
            var list = _context.BrandCars.Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.Id.ToString()
            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Brand)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboModelCars(int brandCarId)
        {
            var brand = _context.BrandCars.Find(brandCarId);
            var list = new List<SelectListItem>();
            if (brand != null)
            {
                list = brand.Model.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).OrderBy(l => l.Text).ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Model...)",
                Value = "0"
            });

            return list;
        }

        public async Task<ModelCar> GetModelCarAsync(int id)
        {
            return await _context.ModelCars.FindAsync(id);
        }

        public async Task<int> UpdateModelCarAsync(ModelCar modelCar)
        {
            var brand = await _context.BrandCars.Where(b => b.Model.Any(m => m.Id == modelCar.Id)).FirstOrDefaultAsync();
            if (brand == null)
            {
                return 0;
            }

            _context.ModelCars.Update(modelCar);
            await _context.SaveChangesAsync();
            return brand.Id;
        }
    }
}
