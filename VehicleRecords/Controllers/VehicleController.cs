using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleRecords.Data;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   public class VehicleController
      : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private IVehicleRepository _repository;
      private IUserRepository _userRepository;

      //   C o n s t r u c t o r s

      public VehicleController(IVehicleRepository repository, IUserRepository userRepository)
      {
         _repository = repository;
         _userRepository = userRepository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add()
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         return View(new Vehicle());
      }

      [HttpPost]
      public IActionResult Add(Vehicle vehicle)
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         if (!ModelState.IsValid)
         {
            return View(vehicle);
         }

         _repository.Add(vehicle);
         return RedirectToAction("Index");
      }

      //   R e a d

      public IActionResult Index()
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         return View(_repository.GetAllVehicles().OrderBy(v => v.Year).ThenBy(v => v.Make).ThenBy(v => v.Vin));
      }

      public IActionResult Detail(int id)
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         Vehicle vehicle = _repository.GetVehicleById(id);
         if (vehicle == null)
         {
            return RedirectToAction("Index");
         }

         return View(vehicle);
      }

      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         Vehicle vehicle = _repository.GetVehicleById(id);
         if (vehicle == null)
         {
            return RedirectToAction("Index");
         }

         return View(vehicle);
      }

      [HttpPost]
      public IActionResult Edit(Vehicle vehicle)
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         if (!ModelState.IsValid)
         {
            return View(vehicle);
         }

         _repository.UpdateVehiclePutEntire(vehicle, vehicle.Id);
         return RedirectToAction("Detail", new { id = vehicle.Id });
      }

      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int id)
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         Vehicle vehicle = _repository.GetVehicleById(id);
         if (vehicle == null)
         {
            return RedirectToAction("Index");
         }

         return View(vehicle);
      }

      [HttpPost]
      public IActionResult Delete(Vehicle vehicle)
      {
         if (!_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Home");
         }

         if (_repository.GetVehicleById(vehicle.Id) == null)
         {
            return RedirectToAction("Index");
         }

         _repository.DeleteVehicle(vehicle.Id);

         return RedirectToAction("Index");
      }
   }
}
