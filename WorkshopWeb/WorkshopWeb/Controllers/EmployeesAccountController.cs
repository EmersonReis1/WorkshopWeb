using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Data.Repositories;
using WorkshopWeb.Helpers;
using WorkshopWeb.Models;

namespace WorkshopWeb.Controllers
{
    public class EmployeesAccountController : Controller
    {
 
        private readonly IUserHelper _userHelper;
        private readonly IGeneratePassword _generatePassword;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly ICountryRepository _countryRepository;

        public EmployeesAccountController(IUserHelper userHelper, IGeneratePassword generatePassword,
            IConfiguration configuration, IMailHelper mailHelper, ICountryRepository countryRepository)
        {
            _userHelper = userHelper;
            _generatePassword = generatePassword;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _countryRepository = countryRepository;
        }

        public IActionResult RegisterEmployees()
        {
            var model = new RegisterNewEmployeesViewModel
            {
                Roles = _userHelper.GetComboRoles(),
                Countries = _countryRepository.GetComboCountries(),
                Cities = _countryRepository.GetComboCities(0)
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployees(RegisterNewEmployeesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                if (user == null)
                {
                    var city = await _countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.UserName,
                        UserName = model.UserName,
                        Nif = model.Nif,
                        CityId = model.CityId,
                        City = city,
                    };

                    var password = await _generatePassword.RandomPassword(4, 2, 1000, 9999);

                    var result = await _userHelper.AddUserAsync(user, password);

                    var isInRole = await _userHelper.IsUserInRoleAsync(user, model.RoleName);
                    if (!isInRole)
                    {
                        await _userHelper.AddUsertoRoleAsync(user, model.RoleName);
                    }

                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.UserName, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"Your Passwor is '{password}', " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";


                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists.");

            }

            return this.RedirectToAction("InfoConfirmEmail", "Account");
        }

        public async Task<JsonResult> GetCitiesAsync(int countryId)
        {
            var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
            return this.Json(country.Cities.OrderBy(c => c.Name));
        }
    }
}
