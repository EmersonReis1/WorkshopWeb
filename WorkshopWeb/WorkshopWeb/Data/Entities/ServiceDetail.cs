using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Data.Entities
{
    public class ServiceDetail : IEntity
    {
        public int Id { get; set; }

        public bool State { get; set; }

        public Service Service { get; set; }

    }
}
