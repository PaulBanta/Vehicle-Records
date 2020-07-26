using Microsoft.AspNetCore.Mvc;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   public class MaintenanceRepairController
      : Controller
   {

      //   F i e l d s   &   P r o p e r t i e s

      private IMaintenanceRepairRepository _repository;

      //   C o n s t r u c t o r s

      public MaintenanceRepairController(IMaintenanceRepairRepository repository)
      {
         _repository = repository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add(int vehicleId)
      {
         MaintenanceRepair maintenanceRepair = new MaintenanceRepair();
         maintenanceRepair.VehicleId = vehicleId;
         return View(maintenanceRepair);
      }

      [HttpPost]
      public IActionResult Add(MaintenanceRepair maintenanceRepair)
      {
         if (ModelState.IsValid)
         {
            _repository.AddMaintenanceRepair(maintenanceRepair);
            return RedirectToAction("Detail", "Vehicle", new { id = maintenanceRepair.VehicleId });
         }

         return View(maintenanceRepair);
      }

      //   R e a d

      // public IActionResult Detail(int id)
      // {
      //    MaintenanceRepair maintenanceRepair = _repository.GetMaintenanceRepairById(id);
      //    if (maintenanceRepair == null)
      //    {
      //       return RedirectToAction("Index", "Vehicle");
      //    }
      //    return View(maintenanceRepair);
      // }

      //   U p d a t e

      // [HttpGet]
      // public IActionResult Edit(int id)
      // {
      //    MaintenanceRepair maintenanceRepair = _repository.GetMaintenanceRepairById(id);
      //    if (maintenanceRepair == null)
      //    {
      //       return RedirectToAction("Index", "Vehicle");
      //    }
      //    return View(maintenanceRepair);
      // }

      // [HttpPost]
      // public IActionResult Edit(MaintenanceRepair maintenanceRepair)
      // {
      //    if (ModelState.IsValid)
      //    {
      //       _repository.UpdateMaintenanceRepairPutEntire(maintenanceRepair, maintenanceRepair.Id);
      //       return RedirectToAction("Detail", "Vehicle", new { id = maintenanceRepair.VehicleId });
      //    }
      //    return View(maintenanceRepair);
      // }

      //   D e l e t e

      // [HttpGet]
      // public IActionResult Delete(int id)
      // {
      //    MaintenanceRepair maintenanceRepair = _repository.GetMaintenanceRepairById(id);
      //    if (maintenanceRepair == null)
      //    {
      //       return RedirectToAction("Index", "Vehicle");
      //    }
      //    return View(maintenanceRepair);
      // }

      // [HttpPost]
      // public IActionResult Delete(MaintenanceRepair maintenanceRepair)
      // {
      //    _repository.DeleteMaintenanceRepair(maintenanceRepair.Id);
      //    return RedirectToAction("Detail", "Vehicle", new { id = maintenanceRepair.VehicleId });
      // }
   }
}
