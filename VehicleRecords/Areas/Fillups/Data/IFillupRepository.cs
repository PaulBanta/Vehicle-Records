using System.Linq;
using VehicleRecords.Areas.Fillups.Models;

namespace VehicleRecords.Areas.Fillups.Data
{
   public interface IFillupRepository
   {
      //   C r e a t e

      public Fillup AddFillup(Fillup fillup);

      public Fillup AddFillup(Fillup fillup, int vehicleId);

      //   R e a d

      public IQueryable<Fillup> GetAllFillups(int vehicleId);

      public Fillup GetFillupById(int id);

      //   U p d a t e

      public Fillup UpdateFillupPatchPartial(Fillup fillup, int id);

      public Fillup UpdateFillupPutEntire(Fillup fillup, int id);

      //   D e l e t e

      public bool DeleteFillup(int id);
   }
}
