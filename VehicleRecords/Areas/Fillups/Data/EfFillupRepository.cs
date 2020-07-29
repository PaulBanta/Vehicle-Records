using System;
using System.Collections.Generic;
using System.Linq;
using VehicleRecords.Models;

namespace VehicleRecords.Data
{
   public class EfFillupRepository
      : IFillupRepository
   {
      //   F i e l d s

      private AppDbContext _context;
      private IUserRepository _userRepository;
      private IVehicleRepository _vehicleRepository;

      //   C o n s t r u c t o r s

      public EfFillupRepository(AppDbContext context, IUserRepository userRepository, IVehicleRepository vehicleRepository)
      {
         _context = context;
         _userRepository = userRepository;
         _vehicleRepository = vehicleRepository;
      }

      //   M e t h o d s

      public Fillup AddFillup(Fillup fillup)
      {
         if (fillup == null || fillup.VehicleId <= 0)
         {
            return null;
         }

         if (_vehicleRepository.VehicleExists(fillup.VehicleId) == false)
         {
            return null;
         }

         _context.Fillups.Add(fillup);
         _context.SaveChanges();

         RecomputeDaysBetweenFillups(fillup.VehicleId);

         return fillup;
      }

      public Fillup AddFillup(Fillup fillup, int vehicleId)
      {
         fillup.VehicleId = vehicleId;
         return AddFillup(fillup);
      }

      //   R e a d

      public IQueryable<Fillup> GetAllFillups(int vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return new List<Fillup>().AsQueryable<Fillup>();
         }

         return _context.Fillups.Where(f => f.VehicleId == vehicleId);
      }

      public Fillup GetFillupById(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         Fillup fillup = _context.Fillups.FirstOrDefault(f => f.Id == id);
         if (_vehicleRepository.VehicleExists(fillup.VehicleId) == false)
         {
            return null;
         }

         return fillup;
      }

      //   U p d a t e

      public Fillup UpdateFillupPatchPartial(Fillup fillup, int id)
      {
         Fillup fillupToUpdate = GetFillupById(id);

         if (fillupToUpdate != null)
         {
            if (fillup.Date != null)
               fillupToUpdate.Date = fillup.Date;
            fillupToUpdate.Gallons = fillup.Gallons;
            fillupToUpdate.Odometer = fillup.Odometer;
            fillupToUpdate.TotalCost = fillup.TotalCost;
            fillupToUpdate.TripOdometer = fillup.TripOdometer;

            _context.SaveChanges();

            RecomputeDaysBetweenFillups(fillupToUpdate.VehicleId);
         }

         return fillupToUpdate;
      }

      public Fillup UpdateFillupPutEntire(Fillup fillup, int id)
      {
         Fillup fillupToUpdate = GetFillupById(id);

         if (fillupToUpdate != null)
         {
            fillupToUpdate.Date = fillup.Date;
            fillupToUpdate.Gallons = fillup.Gallons;
            fillupToUpdate.Odometer = fillup.Odometer;
            fillupToUpdate.TotalCost = fillup.TotalCost;
            fillupToUpdate.TripOdometer = fillup.TripOdometer;

            _context.SaveChanges();

            RecomputeDaysBetweenFillups(fillupToUpdate.VehicleId);
         }

         return fillupToUpdate;
      }

      //   D e l e t e

      public bool DeleteFillup(int id)
      {
         Fillup fillupToDelete = GetFillupById(id);
         if (fillupToDelete == null)
         {
            return false;
         }

         try
         {
            _context.Fillups.Remove(fillupToDelete);
            _context.SaveChanges();

            RecomputeDaysBetweenFillups(fillupToDelete.VehicleId);

            return true;
         }
         catch (Exception)
         {
         }

         return false;
      } // end DeleteFillup( )

      //   P r i v a t e   M e t h o d s

      private void RecomputeDaysBetweenFillups(int vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return;
         }

         IEnumerable<Fillup> fillups = _context.Fillups.Where(f => f.VehicleId == vehicleId)
                                                       .OrderBy(f => f.Date).ThenBy(f => f.Odometer);
         if (fillups == null || fillups.Count() == 0)
         {
            return;
         }

         DateTime? lastDate = fillups.ElementAt(0).Date;
         foreach (Fillup f in fillups)
         {
            f.DaysSinceLastFillup = Math.Max(1, (int) ((f.Date - lastDate).GetValueOrDefault().TotalDays + 0.5));
            lastDate = f.Date;
         }

         _context.SaveChanges();
      } // end RecomputeDaysBetweenFillups( )
   }
}
