using System.Linq;

namespace VehicleRecords.Models
{
   public interface IMaintenanceRepairRepository
   {
      //   C r e a t e

      public MaintenanceRepair AddMaintenanceRepair(MaintenanceRepair maintenanceRepair);

      public MaintenanceRepair AddMaintenanceRepair(MaintenanceRepair maintenanceRepair, int vehicleId);

      //   R e a d

      public IQueryable<MaintenanceRepair> GetAllMaintenanceRepairs(int vehicleId);

      public MaintenanceRepair GetMaintenanceRepairById(int id);

      //   U p d a t e

      public MaintenanceRepair UpdateMaintenanceRepairPatchPartial(MaintenanceRepair maintenanceRepair, int id);

      public MaintenanceRepair UpdateMaintenanceRepairPutEntire(MaintenanceRepair maintenanceRepair, int id);

      //   D e l e t e

      public bool DeleteMaintenanceRepair(int id);
   }
}
