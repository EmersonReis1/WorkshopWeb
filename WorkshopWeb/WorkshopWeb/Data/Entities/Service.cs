using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Data.Entities
{
    public class Service : IEntity
    {
        public int Id { get; set; }



        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
