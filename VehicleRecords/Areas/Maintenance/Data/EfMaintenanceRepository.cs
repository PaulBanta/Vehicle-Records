using System;
using System.Collections.Generic;
using System.Linq;
using VehicleRecords.Models;

namespace VehicleRecords.Data
{
   public class EfMaintenanceRepository
      : IMaintenanceRepository
   {
      //   F i e l d s

      private AppDbContext _context;
      private IUserRepository _userRepository;
      private IVehicleRepository _vehicleRepository;

      //   C o n s t r u c t o r s

      public EfMaintenanceRepository(AppDbContext context, IUserRepository userRepository, IVehicleRepository vehicleRepository)
      {
         _context = context;
         _userRepository = userRepository;
         _vehicleRepository = vehicleRepository;
      }

      //   M e t h o d s

      public Models.Maintenance AddMaintenance(Models.Maintenance maintenance)
      {
         if (maintenance == null || maintenance.VehicleId <= 0)
         {
            return null;
         }

         if (_vehicleRepository.VehicleExists(maintenance.VehicleId) == false)
         {
            return null;
         }

         _context.Maintenance.Add(maintenance);
         _context.SaveChanges();

         return maintenance;
      }

      public Models.Maintenance AddMaintenance(Models.Maintenance maintenance, int vehicleId)
      {
         maintenance.VehicleId = vehicleId;
         return AddMaintenance(maintenance);
      }

      //   R e a d

      public IQueryable<Models.Maintenance> GetAllMaintenance(int vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return new List<Models.Maintenance>().AsQueryable<Models.Maintenance>();
         }

         return _context.Maintenance.Where(f => f.VehicleId == vehicleId);
      }

      public Models.Maintenance GetMaintenanceById(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         Models.Maintenance maintenance = _context.Maintenance.FirstOrDefault(f => f.Id == id);
         if (_vehicleRepository.VehicleExists(maintenance.VehicleId) == false)
         {
            return null;
         }

         return maintenance;
      }

      //   U p d a t e

      public Models.Maintenance UpdateMaintenancePatchPartial(Models.Maintenance maintenance, int id)
      {
         Models.Maintenance maintenanceRepairToUpdate = GetMaintenanceById(id);

         if (maintenanceRepairToUpdate != null)
         {
            if (maintenance.BriefDescriptionOfWork != null)
               maintenanceRepairToUpdate.BriefDescriptionOfWork = maintenance.BriefDescriptionOfWork;
            if (maintenance.Date != null)
               maintenanceRepairToUpdate.Date = maintenance.Date;
            if (maintenance.FullDescriptionOfWork != null)
               maintenanceRepairToUpdate.FullDescriptionOfWork = maintenance.FullDescriptionOfWork;
            if (maintenance.Odometer > 0)
               maintenanceRepairToUpdate.Odometer = maintenance.Odometer;
            if (maintenance.PerformedBy != null)
               maintenanceRepairToUpdate.PerformedBy = maintenance.PerformedBy;
            if (maintenance.TotalCost > 0)
               maintenanceRepairToUpdate.TotalCost = maintenance.TotalCost;

            _context.SaveChanges();
         }

         return maintenanceRepairToUpdate;
      }

      public Models.Maintenance UpdateMaintenancePutEntire(Models.Maintenance maintenance, int id)
      {
         Models.Maintenance maintenanceRepairToUpdate = GetMaintenanceById(id);

         if (maintenanceRepairToUpdate != null)
         {
            maintenanceRepairToUpdate.BriefDescriptionOfWork = maintenance.BriefDescriptionOfWork;
            maintenanceRepairToUpdate.Date = maintenance.Date;
            maintenanceRepairToUpdate.FullDescriptionOfWork = maintenance.FullDescriptionOfWork;
            maintenanceRepairToUpdate.Odometer = maintenance.Odometer;
            maintenanceRepairToUpdate.PerformedBy = maintenance.PerformedBy;
            maintenanceRepairToUpdate.TotalCost = maintenance.TotalCost;

            _context.SaveChanges();
         }

         return maintenanceRepairToUpdate;
      }

      //   D e l e t e

      public bool DeleteMaintenance(int id)
      {
         Models.Maintenance maintenanceRepairToDelete = GetMaintenanceById(id);
         if (maintenanceRepairToDelete == null)
         {
            return false;
         }

         try
         {
            _context.Maintenance.Remove(maintenanceRepairToDelete);
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
