using System.Linq;
// using VehicleRecords.Areas.Insurance.Models; // This doesn't seem to work

/*
** Note: All of the Insurance "stuff" is in its own Area. For some reason the
**       "using" statement above doesn't seem to work. It seems that the
**       compiler cannot distinguish between the Area called Insurance and the
**       POCO also called Insurance. A solution (at least for now) is to put the
**       word(s) "Model." in front of Insurance.
*/

namespace VehicleRecords.Areas.Insurance.Data
{
   public interface IInsuranceRepository
   {
      //   C r e a t e

      public Models.Insurance AddInsurance(Models.Insurance insurance);

      public Models.Insurance AddInsurance(Models.Insurance insurance, int vehicleId);

      //   R e a d

      public IQueryable<Models.Insurance> GetAllInsurance(int vehicleId);

      public Models.Insurance GetInsuranceById(int id);

      //   U p d a t e

      public Models.Insurance UpdateInsurancePatchPartial(Models.Insurance insurance, int id);

      public Models.Insurance UpdateInsurancePutEntire(Models.Insurance insurance, int id);

      //   D e l e t e

      public bool DeleteInsurance(int id);
   }
}
