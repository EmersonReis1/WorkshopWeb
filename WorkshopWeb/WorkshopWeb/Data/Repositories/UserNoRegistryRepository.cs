using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;

namespace WorkshopWeb.Data.Repositories
{
    public class UserNoRegistryRepository : GenericRepository<UserNoRegistry>, IUserNoRegistryRepository
    {
        public UserNoRegistryRepository(DataContext context) : base(context)
        {
        }
    }
}
