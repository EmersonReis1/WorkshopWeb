using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Helpers;

namespace WorkshopWeb.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
           _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
            await _userHelper.CheckRoleAsync("Mechanic");
            await _userHelper.CheckRoleAsync("Recessionist");

            if (!_context.BrandCars.Any())
            {

                //LIST MODELS SEAT
                var listSeat = new List<ModelCar>();

                listSeat.Add(new ModelCar { Name = "Ibiza " });
                listSeat.Add(new ModelCar { Name = "Leon" });
                listSeat.Add(new ModelCar { Name = "Arona " });
                listSeat.Add(new ModelCar { Name = "Ateca" });

                _context.BrandCars.Add( new BrandCar { Name = "SEAT", Model = listSeat}) ;


                //LIST MODELS BMW
                var listBMW = new List<ModelCar>();

                listBMW.Add(new ModelCar { Name = "Série 1" });
                listBMW.Add(new ModelCar { Name = "X8" });
                listBMW.Add(new ModelCar { Name = "M240I Coupé" });
                listBMW.Add(new ModelCar { Name = "I8 Coupé" });

                _context.BrandCars.Add(new BrandCar { Name = "BMW", Model = listBMW });

                //LIST MODELS Toyota
                var listToyota = new List<ModelCar>();

                listToyota.Add(new ModelCar { Name = "Auris" });
                listToyota.Add(new ModelCar { Name = "Yaris" });
                listToyota.Add(new ModelCar { Name = "Corona" });
                listToyota.Add(new ModelCar { Name = "GT-86" });
                listToyota.Add(new ModelCar { Name = "Corolla" });

                _context.BrandCars.Add(new BrandCar { Name = "Toyota", Model = listToyota });

                //LIST MODELS Volvo
                var listVolvo = new List<ModelCar>();

                listVolvo.Add(new ModelCar { Name = "V90" });
                listVolvo.Add(new ModelCar { Name = "Xc40" });
                listVolvo.Add(new ModelCar { Name = "S60" });
                listVolvo.Add(new ModelCar { Name = "V60" });
                listVolvo.Add(new ModelCar { Name = "S90" });
                listVolvo.Add(new ModelCar { Name = "Xc90" });

                _context.BrandCars.Add(new BrandCar { Name = "Volvo", Model = listVolvo });

                //LIST MODELS Volvo
                var listCitroen = new List<ModelCar>();

                listCitroen.Add(new ModelCar { Name = "C1" });
                listCitroen.Add(new ModelCar { Name = "C-Elysee" });
                listCitroen.Add(new ModelCar { Name = "Berlingo" });
                listCitroen.Add(new ModelCar { Name = "C4 Cactus" });
                listCitroen.Add(new ModelCar { Name = "C5 Aircross" });
                listCitroen.Add(new ModelCar { Name = "C-Zero" });

                _context.BrandCars.Add(new BrandCar { Name = "Citroen", Model = listCitroen });

                await _context.SaveChangesAsync();
            }

            if (!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Coimbra" });
                cities.Add(new City { Name = "Faro" });


                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });


                await _context.SaveChangesAsync();
            }

            var user = await _userHelper.GetUserByEmailAsync("emerson.teste.22@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Emerson",
                    LastName = "Reis",
                    Email = "emerson.teste.22@gmail.com",
                    UserName = "emerson.teste.22@gmail.com",
                    PhoneNumber = "223232323",
                    Address = "Rua Da Luz 23 2D",
                    Nif = "1515155",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "123456");



                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }


                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);


                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUsertoRoleAsync(user, "Admin");
                }


            }

        }
    }
}
