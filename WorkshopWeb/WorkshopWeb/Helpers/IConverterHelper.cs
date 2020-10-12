using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Helpers
{
    public interface IConverterHelper
    {
        Car ToCar(CarViewModel model, User user, ModelCar modelCar, bool isNew);


        CarViewModel ToCarViewModel(Car model);
    }
}
