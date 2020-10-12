using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data.Repositories
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        IQueryable<Car> GetAllCar();

        Task<Car> GetCarAsync(int id);

        Task<IEnumerable<Car>> GetCarsWithUserAsync(User user);

        Task<Car> GetCarWithRegistrationPlate(string registrationPlate);

        Task<Car> GetCarWithUserAsync(User user, string registrationPlate);

        Task<Car> GetCarWithNotUserAsync(UserNoRegistry user);

    }
}
