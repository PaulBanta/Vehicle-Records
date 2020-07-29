using Microsoft.AspNetCore.Mvc;
using System;
using VehicleRecords.Areas.Insurance.Data;
using VehicleRecords.Areas.Insurance.Models;

namespace VehicleRecords.Controllers
{
   [Area("Insurance")]
   public class InsuranceController
      : Controller
   {

      //   F i e l d s   &   P r o p e r t i e s

      private IInsuranceRepository _repository;

      //   C o n s t r u c t o r s

      public InsuranceController(IInsuranceRepository repository)
      {
         _repository = repository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add(int vehicleId)
      {
         Insurance insurance = new Insurance();
         insurance.VehicleId = vehicleId;
         insurance.Date = DateTime.Now;
         return View(insurance);
      }

      [HttpPost]
      public IActionResult Add(Insurance insurance)
      {
         if (ModelState.IsValid)
         {
            _repository.AddInsurance(insurance);
            return RedirectToAction("Detail", new { id = insurance.Id });
         }

         return View(insurance);
      }

      //   R e a d

      public IActionResult Detail(int id)
      {
         Insurance insurance = _repository.GetInsuranceById(id);
         if (insurance == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(insurance);
      }

      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         Insurance insurance = _repository.GetInsuranceById(id);
         if (insurance == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(insurance);
      }

      [HttpPost]
      public IActionResult Edit(Insurance insurance)
      {
         if (ModelState.IsValid)
         {
            _repository.UpdateInsurancePutEntire(insurance, insurance.Id);
            return RedirectToAction("Detail", new { id = insurance.Id });
         }
         return View(insurance);
      }

      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int id)
      {
         Insurance insurance = _repository.GetInsuranceById(id);
         if (insurance == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(insurance);
      }

      [HttpPost]
      public IActionResult Delete(Insurance insurance)
      {
         _repository.DeleteInsurance(insurance.Id);
         return Redirect($"/Vehicle/Detail/?id={insurance.VehicleId}");
      }
   }
}
