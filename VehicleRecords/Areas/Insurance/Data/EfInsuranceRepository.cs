using System;
using System.Collections.Generic;
using System.Linq;
// using VehicleRecords.Areas.Insurance.Models; // This doesn't seem to work
using VehicleRecords.Models;

/*
** Note: All of the Insurance "stuff" is in its own Area. For some reason the
**       "using" statement above doesn't seem to work. It seems that the
**       compiler cannot distinguish between the Area called Insurance and the
**       POCO also called Insurance. A solution (at least for now) is to put the
**       word(s) "Model." in front of Insurance.
*/

namespace VehicleRecords.Data
{
   public class EfInsuranceRepository
      : IInsuranceRepository
   {
      //   F i e l d s

      private AppDbContext _context;
      private IUserRepository _userRepository;
      private IVehicleRepository _vehicleRepository;

      //   C o n s t r u c t o r s

      public EfInsuranceRepository(AppDbContext context, IUserRepository userRepository, IVehicleRepository vehicleRepository)
      {
         _context = context;
         _userRepository = userRepository;
         _vehicleRepository = vehicleRepository;
      }

      //   M e t h o d s

      //   C r e a t e

      public Models.Insurance AddInsurance(Models.Insurance insurance)
      {
         if (insurance == null || insurance.Id > 0 || insurance.VehicleId <= 0 || _vehicleRepository.VehicleExists(insurance.VehicleId) == false)
         {
            return null;
         }

         _context.Insurance.Add(insurance);
         _context.SaveChanges();

         return insurance;
      }

      public Models.Insurance AddInsurance(Models.Insurance insurance, int vehicleId)
      {
         insurance.VehicleId = vehicleId;
         return AddInsurance(insurance);
      }

      //   R e a d

      public IQueryable<Models.Insurance> GetAllInsurance(int vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return new List<Models.Insurance>().AsQueryable<Models.Insurance>();
         }

         return _context.Insurance.Where(f => f.VehicleId == vehicleId);
      }

      public Models.Insurance GetInsuranceById(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         Models.Insurance insurance = _context.Insurance.FirstOrDefault(i => i.Id == id);
         if (insurance == null || _vehicleRepository.VehicleExists(insurance.VehicleId) == false)
         {
            return null;
         }

         return insurance;
      }

      //   U p d a t e

      public Models.Insurance UpdateInsurancePatchPartial(Models.Insurance insurance, int id)
      {
         Models.Insurance insuranceToUpdate = GetInsuranceById(id);

         if (insuranceToUpdate != null)
         {
            if (insurance.Company != null)
               insuranceToUpdate.Company = insurance.Company;
            if (insurance.Coverage != null)
               insuranceToUpdate.Coverage = insurance.Coverage;
            if (insurance.Date != null)
               insuranceToUpdate.Date = insurance.Date;
            if (insurance.NumberOfMonths > 0)
               insuranceToUpdate.NumberOfMonths = insurance.NumberOfMonths;
            if (insurance.PolicyNumber != null)
               insuranceToUpdate.PolicyNumber = insurance.PolicyNumber;
            if (insurance.TotalCost > 0)
               insuranceToUpdate.TotalCost = insurance.TotalCost;

            _context.SaveChanges();
         }

         return insuranceToUpdate;
      }

      public Models.Insurance UpdateInsurancePutEntire(Models.Insurance insurance, int id)
      {
         Models.Insurance insuranceToUpdate = GetInsuranceById(id);

         if (insuranceToUpdate != null)
         {
            insuranceToUpdate.Company = insurance.Company;
            insuranceToUpdate.Coverage = insurance.Coverage;
            insuranceToUpdate.Date = insurance.Date;
            insuranceToUpdate.NumberOfMonths = insurance.NumberOfMonths;
            insuranceToUpdate.PolicyNumber = insurance.PolicyNumber;
            insuranceToUpdate.TotalCost = insurance.TotalCost;

            _context.SaveChanges();
         }

         return insuranceToUpdate;
      }

      //   D e l e t e

      public bool DeleteInsurance(int id)
      {
         Models.Insurance insuranceToDelete = GetInsuranceById(id);
         if (insuranceToDelete == null)
         {
            return false;
         }

         try
         {
            _context.Insurance.Remove(insuranceToDelete);
            _context.SaveChanges();
            return true;
         }
         catch (Exception)
         {
         }

         return false;
      } // end DeleteInsurance( )
   }
}
