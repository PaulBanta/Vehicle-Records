using System.Linq;
// using VehicleRecords.Areas.Registration.Models; // This doesn't seem to work

/*
** Note: All of the Registration "stuff" is in its own Area. For some reason the
**       "using" statement above doesn't seem to work. It seems that the
**       compiler cannot distinguish between the Area called Registration and the
**       POCO also called Registration. A solution (at least for now) is to put the
**       word(s) "Model." in front of Registration.
*/

namespace VehicleRecords.Data
{
   public interface IRegistrationRepository
   {
      //   C r e a t e

      public Models.Registration AddRegistration(Models.Registration insurance);

      public Models.Registration AddRegistration(Models.Registration insurance, int vehicleId);

      //   R e a d

      public IQueryable<Models.Registration> GetAllRegistration(int vehicleId);

      public Models.Registration GetRegistrationById(int id);

      //   U p d a t e

      public Models.Registration UpdateRegistrationPatchPartial(Models.Registration insurance, int id);

      public Models.Registration UpdateRegistrationPutEntire(Models.Registration insurance, int id);

      //   D e l e t e

      public bool DeleteRegistration(int id);
   }
}
