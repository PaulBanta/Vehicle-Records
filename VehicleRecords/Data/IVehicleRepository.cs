using System.Linq;
using VehicleRecords.Models;

namespace VehicleRecords.Data
{
   public interface IVehicleRepository
   {
      //   C r e a t e

      public Vehicle Add(Vehicle vehicle);

      //   R e a d

      public IQueryable<Vehicle> GetAllVehicles();

      public Vehicle GetVehicleById(int id);

      public bool VehicleExists(int vehicleId);

      //   U p d a t e

      public Vehicle UpdateVehiclePatchPartial(Vehicle vehicle, int id);

      public Vehicle UpdateVehiclePutEntire(Vehicle vehicle, int id);

      //   D e l e t e

      public bool DeleteVehicle(int id);
   }
}
