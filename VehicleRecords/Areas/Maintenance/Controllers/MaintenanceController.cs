using Microsoft.AspNetCore.Mvc;
using System;
using VehicleRecords.Data;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   [Area("Maintenance")]
   public class MaintenanceController
      : Controller
   {

      //   F i e l d s   &   P r o p e r t i e s

      private IMaintenanceRepository _repository;

      //   C o n s t r u c t o r s

      public MaintenanceController(IMaintenanceRepository repository)
      {
         _repository = repository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add(int vehicleId)
      {
         Maintenance maintenance = new Maintenance();
         maintenance.VehicleId = vehicleId;
         maintenance.Date = DateTime.Now;
         return View(maintenance);
      }

      [HttpPost]
      public IActionResult Add(Maintenance maintenance)
      {
         if (ModelState.IsValid)
         {
            _repository.AddMaintenance(maintenance);
            return RedirectToAction("Detail", new { id = maintenance.Id });
         }

         return View(maintenance);
      }

      //   R e a d

      public IActionResult Detail(int id)
      {
         Maintenance maintenance = _repository.GetMaintenanceById(id);
         if (maintenance == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(maintenance);
      }

      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         Maintenance maintenance = _repository.GetMaintenanceById(id);
         if (maintenance == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(maintenance);
      }

      [HttpPost]
      public IActionResult Edit(Maintenance maintenance)
      {
         if (ModelState.IsValid)
         {
            _repository.UpdateMaintenancePutEntire(maintenance, maintenance.Id);
            return RedirectToAction("Detail", new { id = maintenance.VehicleId });
         }
         return View(maintenance);
      }

      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int id)
      {
         Maintenance maintenance = _repository.GetMaintenanceById(id);
         if (maintenance == null)
         {
            return Redirect("/Vehicle/Index");
         }
         return View(maintenance);
      }

      [HttpPost]
      public IActionResult Delete(Maintenance maintenance)
      {
         _repository.DeleteMaintenance(maintenance.Id);
         return Redirect($"/Vehicle/Detail/?id={maintenance.VehicleId}");
      }
   }
}
