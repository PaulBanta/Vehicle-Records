using Microsoft.AspNetCore.Mvc;
using System;
using VehicleRecords.Data;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   [Area("Fillups")]
   public class FillupController
      : Controller
   {

      //   F i e l d s   &   P r o p e r t i e s

      private IFillupRepository _repository;

      //   C o n s t r u c t o r s

      public FillupController(IFillupRepository repository)
      {
         _repository = repository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add(int vehicleId)
      {
         Fillup fillup = new Fillup();
         fillup.VehicleId = vehicleId;
         fillup.Date = DateTime.Now;
         return View(fillup);
      }

      [HttpPost]
      public IActionResult Add(Fillup fillup)
      {
         if (ModelState.IsValid)
         {
            _repository.AddFillup(fillup);
            return RedirectToAction("Detail", new { id = fillup.Id });
         }

         return View(fillup);
      }

      //   R e a d

      public IActionResult Detail(int id)
      {
         Fillup fillup = _repository.GetFillupById(id);
         if (fillup == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(fillup);
      }

      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         Fillup fillup = _repository.GetFillupById(id);
         if (fillup == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(fillup);
      }

      [HttpPost]
      public IActionResult Edit(Fillup fillup)
      {
         if (ModelState.IsValid)
         {
            _repository.UpdateFillupPutEntire(fillup, fillup.Id);
            return RedirectToAction("Detail", new { id = fillup.Id });
         }
         return View(fillup);
      }

      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int id)
      {
         Fillup fillup = _repository.GetFillupById(id);
         if (fillup == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(fillup);
      }

      [HttpPost]
      public IActionResult Delete(Fillup fillup)
      {
         _repository.DeleteFillup(fillup.Id);
         return Redirect("/Vehicle/Detail/?id=" + fillup.VehicleId);
      }
   }
}
