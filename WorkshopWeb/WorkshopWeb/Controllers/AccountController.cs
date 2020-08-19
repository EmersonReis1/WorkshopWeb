using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Data.Repositories;
using WorkshopWeb.Helpers;
using WorkshopWeb.Models;

namespace WorkshopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;

        public AccountController(IUserHelper userHelper, ICountryRepository countryRepository, IConfiguration configuration, IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _configuration = configuration;
            _mailHelper = mailHelper;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var country = _countryRepository.GetComboCountries();

            if (country.Count() == 2 )
            {
                var model1 = new RegisterNewUserViewModel
                {
                    Countries = country,
                    Cities = _countryRepository.GetListCities(Convert.ToInt32(country.Last().Value))
                };

                return this.View(model1);
            }

            var model = new RegisterNewUserViewModel
            {
                Countries = country,
                Cities = _countryRepository.GetComboCities(0)
            };

            return this.View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
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
                        Nif = model.Nif,
                        UserName = model.UserName,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CityId = model.CityId,
                        City = city

                    };


                    var result = await _userHelper.AddUserAsync(user, model.Password);

                    var isInRole = await _userHelper.IsUserInRoleAsync(user, "Customer");
                    if (!isInRole)
                    {
                        await _userHelper.AddUsertoRoleAsync(user, "Customer");
                    }

                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }
                    var ff = this.Url;
                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.UserName, "Email confirmation", _mailHelper.BodyMailConfirmation(tokenLink, model.FirstName, this.Url.RouteUrl("~/")));

                    //var ig = this.Url.Link
                    //_mailHelper.SendMail(model.UserName, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                    //    $"To allow the user, " +
                    //    $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";


                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists.");

            }

            return this.RedirectToAction("InfoConfirmEmail", "Account");
        }



        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            return View();
        }

        public IActionResult InfoConfirmEmail()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                             _configuration["Tokens:Issuer"],
                             _configuration["Tokens:Audience"],
                             claims,
                             expires: DateTime.UtcNow.AddDays(15),
                             signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                //foreach (var item in _userHelper.GetComboRoles())
                //{
                //    if (await _userHelper.IsUserInRoleAsync(user, item.Value) && item.Value != "Customer")
                //    {
                //        this.ViewBag.Message = "Admin";
                //    }
                //}

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action("ResetPassword", "Account",  new { token = myToken, email = user.Email }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "ER.Auto Password Reset", _mailHelper.BodyMailConfirmation(link, user.FullName, "https://localhost:44399/"));

                this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return this.View();
            }

            return View();
        }

        public IActionResult ResetPassword(string token, string email)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public async Task<JsonResult> GetCitiesAsync(int countryId)
        {
            var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
            return this.Json(country.Cities.OrderBy(c => c.Name));
        }
    }
}
