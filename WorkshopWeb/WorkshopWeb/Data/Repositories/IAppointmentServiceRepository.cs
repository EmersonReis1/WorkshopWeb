using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Data.Repositories
{
    public interface IAppointmentServiceRepository : IGenericRepository<AppointmentService>
    {
        Task<IQueryable<AppointmentService>> GetAppointmentAsync(string userName);

        IQueryable<AppointmentService> GetAppointmentById(int id);

        Task<bool> AddAppointmentAsync(AddAppointmentViewModel model, User user);

        Task<bool> DeleteAppointmentAsync(int id);


    }
}
