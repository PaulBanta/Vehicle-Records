using Microsoft.AspNetCore.Mvc;
using System;
using VehicleRecords.Data;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   [Area("Registrations")]
   public class RegistrationController
      : Controller
   {

      //   F i e l d s   &   P r o p e r t i e s

      private IRegistrationRepository _repository;

      //   C o n s t r u c t o r s

      public RegistrationController(IRegistrationRepository repository)
      {
         _repository = repository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add(int vehicleId)
      {
         Registration registration = new Registration();
         registration.VehicleId = vehicleId;
         registration.Date = DateTime.Now;
         return View(registration);
      }

      [HttpPost]
      public IActionResult Add(Registration registration)
      {
         if (ModelState.IsValid)
         {
            _repository.AddRegistration(registration);
            return RedirectToAction("Detail", new { id = registration.Id });
         }

         return View(registration);
      }

      //   R e a d

      public IActionResult Detail(int id)
      {
         Registration registration = _repository.GetRegistrationById(id);
         if (registration == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(registration);
      }

      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         Registration registration = _repository.GetRegistrationById(id);
         if (registration == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(registration);
      }

      [HttpPost]
      public IActionResult Edit(Registration registration)
      {
         if (ModelState.IsValid)
         {
            _repository.UpdateRegistrationPutEntire(registration, registration.Id);
            return RedirectToAction("Detail", new { id = registration.Id });
         }
         return View(registration);
      }

      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int id)
      {
         Registration registration = _repository.GetRegistrationById(id);
         if (registration == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(registration);
      }

      [HttpPost]
      public IActionResult Delete(Registration registration)
      {
         _repository.DeleteRegistration(registration.Id);
         return Redirect($"/Vehicle/Detail/?id={registration.VehicleId}");
      }
   }
}
