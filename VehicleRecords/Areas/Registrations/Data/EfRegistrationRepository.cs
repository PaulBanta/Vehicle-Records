using System;
using System.Collections.Generic;
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
   public class EfRegistrationRepository
      : IRegistrationRepository
   {
      //   F i e l d s

      private AppDbContext _context;
      private IUserRepository _userRepository;
      private IVehicleRepository _vehicleRepository;

      //   C o n s t r u c t o r s

      public EfRegistrationRepository(AppDbContext context, IUserRepository userRepository, IVehicleRepository vehicleRepository)
      {
         _context = context;
         _userRepository = userRepository;
         _vehicleRepository = vehicleRepository;
      }

      //   M e t h o d s

      //   C r e a t e

      public Models.Registration AddRegistration(Models.Registration registration)
      {
         if (registration == null || registration.Id > 0 || registration.VehicleId <= 0 || _vehicleRepository.VehicleExists(registration.VehicleId) == false)
         {
            return null;
         }

         _context.Registration.Add(registration);
         _context.SaveChanges();

         return registration;
      }

      public Models.Registration AddRegistration(Models.Registration registration, int vehicleId)
      {
         registration.VehicleId = vehicleId;
         return AddRegistration(registration);
      }

      //   R e a d

      public IQueryable<Models.Registration> GetAllRegistration(int vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return new List<Models.Registration>().AsQueryable<Models.Registration>();
         }

         return _context.Registration.Where(f => f.VehicleId == vehicleId);
      }

      public Models.Registration GetRegistrationById(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         Models.Registration registration = _context.Registration.FirstOrDefault(r => r.Id == id);
         if (registration == null || _vehicleRepository.VehicleExists(registration.VehicleId) == false)
         {
            return null;
         }

         return registration;
      }

      //   U p d a t e

      public Models.Registration UpdateRegistrationPatchPartial(Models.Registration registration, int id)
      {
         Models.Registration registrationToUpdate = GetRegistrationById(id);

         if (registrationToUpdate != null)
         {
            if (registration.Date != null)
               registrationToUpdate.Date = registration.Date;
            if (registration.State != null)
               registrationToUpdate.State = registration.State;
            if (registration.TotalCost > 0)
               registrationToUpdate.TotalCost = registration.TotalCost;

            _context.SaveChanges();
         }

         return registrationToUpdate;
      }

      public Models.Registration UpdateRegistrationPutEntire(Models.Registration registration, int id)
      {
         Models.Registration registrationToUpdate = GetRegistrationById(id);

         if (registrationToUpdate != null)
         {
            registrationToUpdate.Date = registration.Date;
            registrationToUpdate.State = registration.State;
            registrationToUpdate.TotalCost = registration.TotalCost;

            _context.SaveChanges();
         }

         return registrationToUpdate;
      }

      //   D e l e t e

      public bool DeleteRegistration(int id)
      {
         Models.Registration registrationToDelete = GetRegistrationById(id);
         if (registrationToDelete == null)
         {
            return false;
         }

         try
         {
            _context.Registration.Remove(registrationToDelete);
            _context.SaveChanges();
            return true;
         }
         catch (Exception)
         {
         }

         return false;
      } // end DeleteRegistration( )
   }
}
