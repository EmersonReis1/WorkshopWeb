using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopWeb.Data.Entities;
using WorkshopWeb.Data.Repositories;
using WorkshopWeb.Helpers;

namespace WorkshopWeb.Controllers
{
    public class ReparationsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IReparationRepository _reparationRepository;
        private readonly IUserHelper _userHelper;

        public ReparationsController(ICarRepository carRepository, IReparationRepository reparationRepository,
            IUserHelper userHelper)
        {
            _carRepository = carRepository;
            _reparationRepository = reparationRepository;
            _userHelper = userHelper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Mechanic"))
            {
                var model = await _reparationRepository.GetReparationViewModel(user);

                return View(model);
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Mechanic"))
            {
                var model = await _reparationRepository.GetReparationByIdAsync(id.Value);

                //return RedirectToAction(nameof(Index));
                return View(model);
            }

            return RedirectToAction("NotAuthorized", "Account");
        }

        
        public async Task<IActionResult> Save( Reparation model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Mechanic"))
            {
                //var model = await _reparationRepository.GetByIdAsync(id.Value);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("NotAuthorized", "Account");
        }


        [Authorize]
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("NotAuthorized", "Account");
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin") || await _userHelper.IsUserInRoleAsync(user, "Mechanic"))
            {
                var model = await _reparationRepository.GetByIdAsync(id.Value);

                model.State = "Execution";
                model.UserEmployee = user;

                await _reparationRepository.UpdateAsync(model);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("NotAuthorized", "Account");
        }
    }
}
