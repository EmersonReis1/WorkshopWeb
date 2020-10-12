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
        private readonly IApiCarHelper _apiCarHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IApiCarHelper apiCarHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _apiCarHelper = apiCarHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
            await _userHelper.CheckRoleAsync("Mechanic");
            await _userHelper.CheckRoleAsync("Recessionist");

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

            if (!_context.BrandCars.Any())
            {

                
                var list = new List<BrandCar>();

                var apiList = await _apiCarHelper.GetBrandsApiAsync();

                foreach (var item in apiList)
                {
                    var listModel = new List<ModelCar>();
                   
                        var model = await _apiCarHelper.GetModelsApiAsync(item.Id);

                        if (model != null)
                        {
                            foreach (var itemModel in model)
                            {
                                listModel.Add(new ModelCar { Name = itemModel.Name });

                            }

                        }
                   
                    
                    list.Add(new BrandCar { Name = item.Name, Model = listModel });

                    if (list.Count() == 59)
                    {
                        break;
                    }
                }


                await _context.BrandCars.AddRangeAsync(list);

                await _context.SaveChangesAsync();
            }

            if (!_context.Services.Any())
            {
                var services = new List<Service>();
                services.Add(new Service { Name = "Oil change and levels" });
                services.Add(new Service { Name = "Shock Absorbers" });
                services.Add(new Service { Name = "Brakes" });
                services.Add(new Service { Name = "Filters" });
                services.Add(new Service { Name = "Air conditioning" });
                services.Add(new Service { Name = "Tires" });
                services.Add(new Service { Name = "Battery" });
                services.Add(new Service { Name = "Outros" });
                services.Add(new Service { Name = "Alignment" });
                services.Add(new Service { Name = "Distribution KITS" });
                services.Add(new Service { Name = "Embraiagems" });
                services.Add(new Service { Name = "Candles" });

                await _context.Services.AddRangeAsync(services);

                await _context.SaveChangesAsync();
            }

            if (!_context.AddressWorkshops.Any())
            {
                var Address = new List<AddressWorkshop>();
                Address.Add(new AddressWorkshop { Name = "ER.Auto Lisboa", Address = "rua 1", City = new City { Name = "Lisboa" }, CityId = 1 });
                Address.Add(new AddressWorkshop { Name = "ER.Auto Porto", Address = "rua 1", City = new City { Name = "Porto" }, CityId = 2 });


                await _context.AddressWorkshops.AddRangeAsync(Address);

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

                var result = await _userHelper.AddUserAsync(user, "EReis1234");



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
