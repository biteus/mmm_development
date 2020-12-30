using MedicalManager.Models;
using MedicalManager.Models.Interfaces;
using MedicalManager.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMedication _handlerMedication;
        private readonly UserManager<User> _userManager;
        //IdentityUser user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        //public HomeController(IMedication repositoryMedication, ILogger<HomeController> logger)
        //{
        //    this._logger = logger;
        //    this._handlerMedication = repositoryMedication;
        //}
        public HomeController(IMedication repositoryMedication, UserManager<User> userManager, ILogger<HomeController> logger)
        {
            this._logger = logger;
            this._handlerMedication = repositoryMedication;
            this._userManager = userManager;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            var userID = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userID)){
                var model = _handlerMedication.GetAllMedication(userID);
                return View(model);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create(Medication medication)
        {
            if (!ModelState.IsValid) return View();
            var userID = _userManager.GetUserId(User);
            var medicationAdded = _handlerMedication.AddMedication(medication, userID);
            return RedirectToAction("details", new { id = medicationAdded.Id });

        }

        [Route("Delete")]
        public IActionResult Delete(int? id)
        {
            var homeDetailsViewModel = new HomeDetailsViewModel
            {   
                Medication = _handlerMedication.DeleteMedication(id ?? 1, _userManager.GetUserId(User)),
                Title = "Medication Delete"
            };
            return View(homeDetailsViewModel);
        }


        [Route("List")]
        public IActionResult List()
        {
            var modelMedicationListView = new MedicationListViewModel
            {
                Medications = _handlerMedication.GetAllMedication(_userManager.GetUserId(User)),
                Title = "Medication List"
            };
            return View(modelMedicationListView);
        }

        //[Route("Edit")]
        //public IActionResult Edit(int? id)
        //{
        //    if (!ModelState.IsValid) return View();
        //    var medicationUpdated = _handlerMedication.UpdateMedication((int) id, _userManager.GetUserId(User));
        //    return RedirectToAction("Details", new { id = medicationUpdated.Id });
        //}

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                Medication = _handlerMedication.GetMedication((int)id, _userManager.GetUserId(User)),
                Title = "Medication Edit"
            };
            return View(homeDetailsViewModel.Medication);
        }

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(Medication medication)
        {
            if (!ModelState.IsValid) return View();
            var medicationUpdated = _handlerMedication.UpdateMedication(medication, _userManager.GetUserId(User));
            return RedirectToAction("Details", new { id = medicationUpdated.Id });

        }

        [Route("Details")]
        public IActionResult Details(int? id)
        {
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                Medication = _handlerMedication.GetMedication((int)id, _userManager.GetUserId(User)),
                Title = "Medication Details"
            };
            return View(homeDetailsViewModel);
        }
    }
}
