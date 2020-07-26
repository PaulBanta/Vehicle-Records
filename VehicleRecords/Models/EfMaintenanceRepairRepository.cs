using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleRecords.Models
{
   public class EfMaintenanceRepairRepository
      : IMaintenanceRepairRepository
   {
      //   F i e l d s

      private AppDbContext _context;
      private IUserRepository _userRepository;
      private IVehicleRepository _vehicleRepository;

      //   C o n s t r u c t o r s

      public EfMaintenanceRepairRepository(AppDbContext context, IUserRepository userRepository, IVehicleRepository vehicleRepository)
      {
         _context = context;
         _userRepository = userRepository;
         _vehicleRepository = vehicleRepository;
      }

      //   M e t h o d s

      public MaintenanceRepair AddMaintenanceRepair(MaintenanceRepair maintenanceRepair)
      {
         if (maintenanceRepair == null || maintenanceRepair.VehicleId <= 0)
         {
            return null;
         }

         if (_vehicleRepository.VehicleExists(maintenanceRepair.VehicleId) == false)
         {
            return null;
         }

         _context.MaintenanceRepairs.Add(maintenanceRepair);
         _context.SaveChanges();

         return maintenanceRepair;
      }

      public MaintenanceRepair AddMaintenanceRepair(MaintenanceRepair maintenanceRepair, int vehicleId)
      {
         maintenanceRepair.VehicleId = vehicleId;
         return AddMaintenanceRepair(maintenanceRepair);
      }

      //   R e a d

      public IQueryable<MaintenanceRepair> GetAllMaintenanceRepairs(int vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return new List<MaintenanceRepair>().AsQueryable<MaintenanceRepair>();
         }

         return _context.MaintenanceRepairs.Where(f => f.VehicleId == vehicleId);
      }

      public MaintenanceRepair GetMaintenanceRepairById(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         MaintenanceRepair maintenanceRepair = _context.MaintenanceRepairs.FirstOrDefault(f => f.Id == id);
         if (_vehicleRepository.VehicleExists(maintenanceRepair.VehicleId) == false)
         {
            return null;
         }

         return maintenanceRepair;
      }

      //   U p d a t e

      public MaintenanceRepair UpdateMaintenanceRepairPatchPartial(MaintenanceRepair maintenanceRepair, int id)
      {
         MaintenanceRepair maintenanceRepairToUpdate = GetMaintenanceRepairById(id);

         if (maintenanceRepairToUpdate != null)
         {
            if (maintenanceRepair.BriefDescriptionOfWork != null)
               maintenanceRepairToUpdate.BriefDescriptionOfWork = maintenanceRepair.BriefDescriptionOfWork;
            if (maintenanceRepair.Date != null)
               maintenanceRepairToUpdate.Date = maintenanceRepair.Date;
            if (maintenanceRepair.FullDescriptionOfWork != null)
               maintenanceRepairToUpdate.FullDescriptionOfWork = maintenanceRepair.FullDescriptionOfWork;
            if (maintenanceRepair.Odometer > 0)
               maintenanceRepairToUpdate.Odometer = maintenanceRepair.Odometer;
            if (maintenanceRepair.PerformedBy != null)
               maintenanceRepairToUpdate.PerformedBy = maintenanceRepair.PerformedBy;
            if (maintenanceRepair.TotalCost > 0)
               maintenanceRepairToUpdate.TotalCost = maintenanceRepair.TotalCost;

            _context.SaveChanges();
         }

         return maintenanceRepairToUpdate;
      }

      public MaintenanceRepair UpdateMaintenanceRepairPutEntire(MaintenanceRepair maintenanceRepair, int id)
      {
         MaintenanceRepair maintenanceRepairToUpdate = GetMaintenanceRepairById(id);

         if (maintenanceRepairToUpdate != null)
         {
            maintenanceRepairToUpdate.BriefDescriptionOfWork = maintenanceRepair.BriefDescriptionOfWork;
            maintenanceRepairToUpdate.Date = maintenanceRepair.Date;
            maintenanceRepairToUpdate.FullDescriptionOfWork = maintenanceRepair.FullDescriptionOfWork;
            maintenanceRepairToUpdate.Odometer = maintenanceRepair.Odometer;
            maintenanceRepairToUpdate.PerformedBy = maintenanceRepair.PerformedBy;
            maintenanceRepairToUpdate.TotalCost = maintenanceRepair.TotalCost;

            _context.SaveChanges();
         }

         return maintenanceRepairToUpdate;
      }

      //   D e l e t e

      public bool DeleteMaintenanceRepair(int id)
      {
         MaintenanceRepair maintenanceRepairToDelete = GetMaintenanceRepairById(id);
         if (maintenanceRepairToDelete == null)
         {
            return false;
         }

         try
         {
            _context.MaintenanceRepairs.Remove(maintenanceRepairToDelete);
            _context.SaveChanges();

            return true;
         }
         catch (Exception)
         {
         }

         return false;
      } // end DeleteMaintenanceRepair( )
   }
}
