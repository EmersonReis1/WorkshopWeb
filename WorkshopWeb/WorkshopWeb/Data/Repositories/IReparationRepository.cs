using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Data.Repositories
{
    public interface IReparationRepository : IGenericRepository<Reparation>
    {
        Task<bool> AddReparationsAsync(AppointmentService AppointmentService);

        //Task<IEnumerable<Reparation>> GetAllReparation();

        //Task<IEnumerable<Reparation>> GetReparationWithUser(User user);

        Task<ReparationViewModel> GetReparationViewModel(User user);

        Task<Reparation> GetReparationByIdAsync(int id);
    }
}
