using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Models;

namespace WorkshopWeb.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Car ToCar(CarViewModel model, User user, ModelCar modelCar, bool isNew)
        {
            return new Car
            {
                Id = isNew ? 0 : model.Id,
                User = user,
                ModelCar = modelCar,
                RegistrationPlate = model.RegistrationPlate,
            };
        }

        public CarViewModel ToCarViewModel(Car model)
        {
            return new CarViewModel 
            { 
                RegistrationPlate = model.RegistrationPlate,
            };        
        }
    }
}
