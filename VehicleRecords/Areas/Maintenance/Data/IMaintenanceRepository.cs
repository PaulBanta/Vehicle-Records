using System.Linq;

namespace VehicleRecords.Data
{
   public interface IMaintenanceRepository
   {
      //   C r e a t e

      public Models.Maintenance AddMaintenance(Models.Maintenance maintenanceRepair);

      public Models.Maintenance AddMaintenance(Models.Maintenance maintenanceRepair, int vehicleId);

      //   R e a d

      public IQueryable<Models.Maintenance> GetAllMaintenance(int vehicleId);

      public Models.Maintenance GetMaintenanceById(int id);

      //   U p d a t e

      public Models.Maintenance UpdateMaintenancePatchPartial(Models.Maintenance maintenanceRepair, int id);

      public Models.Maintenance UpdateMaintenancePutEntire(Models.Maintenance maintenanceRepair, int id);

      //   D e l e t e

      public bool DeleteMaintenance(int id);
   }
}
