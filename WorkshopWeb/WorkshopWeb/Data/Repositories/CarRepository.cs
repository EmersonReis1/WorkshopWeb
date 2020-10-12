using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Car> GetAllCar()
        {
            return _context.Cars
                .Include(u => u.User)
                .Include(n => n.UserNoRegistry)
                .Include(m => m.ModelCar)
                .OrderByDescending(o => o.ModelCar.Name);
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _context.Cars.Include(c => c.ModelCar).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsWithUserAsync(User user)
        {
            return await _context.Cars.Include(c => c.ModelCar).Where(u => u.User == user).ToListAsync();
        }

        public async Task<Car> GetCarWithNotUserAsync(UserNoRegistry user)
        {
            return await _context.Cars.Where(c => c.UserNoRegistry == user).FirstOrDefaultAsync();
        }

        public async Task<Car> GetCarWithRegistrationPlate(string registrationPlate)
        {
            return await _context.Cars.Where(c => c.RegistrationPlate == registrationPlate).FirstOrDefaultAsync();
        }

        public async Task<Car> GetCarWithUserAsync(User user, string registrationPlate)
        {
            return await _context.Cars.Where(c => c.User == user && c.RegistrationPlate == registrationPlate).FirstOrDefaultAsync();
        }
    }
}
