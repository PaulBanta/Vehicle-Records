using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleRecords.Models
{
   public class EfVehicleRepository
      : IVehicleRepository
   {
      //   F i e l d s

      private AppDbContext _context;
      private IUserRepository _userRepository;

      //   C o n s t r u c t o r s

      public EfVehicleRepository(AppDbContext context, IUserRepository userRepository)
      {
         _context = context;
         _userRepository = userRepository;
      }

      //   C r e a t e

      public Vehicle Add(Vehicle vehicle)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         vehicle.UserId = (int)_userRepository.GetLoggedInUserId();
         _context.Vehicles.Add(vehicle);
         _context.SaveChanges();

         return vehicle;
      }

      //   R e a d

      public IQueryable<Vehicle> GetAllVehicles()
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return new List<Vehicle>().AsQueryable();
         }

         return _context.Vehicles.Where(v => v.UserId == _userRepository.GetLoggedInUserId());
      }

      public Vehicle GetVehicleById(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         return _context.Vehicles.Include(f => f.Fillups)
                                 .Include(f => f.MaintenanceRepairs)
                                 .FirstOrDefault(v => v.Id == id && v.UserId == _userRepository.GetLoggedInUserId());
      }

      public bool VehicleExists(int vehicleId)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return false;
         }

         return _context.Vehicles.Any(v => v.Id == vehicleId && v.UserId == _userRepository.GetLoggedInUserId());
      }

      //   U p d a t e

      public Vehicle UpdateVehiclePatchPartial(Vehicle vehicle, int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         Vehicle vehicleToUpdate = GetVehicleById(id);
         if (vehicleToUpdate != null)
         {
            if (vehicle.Color != null)
               vehicleToUpdate.Color = vehicle.Color;
            if (vehicle.DatePurchased != null)
               vehicleToUpdate.DatePurchased = vehicle.DatePurchased;
            if (vehicle.DateSold != null)
               vehicleToUpdate.DateSold = vehicle.DateSold;
            if (vehicle.Make != null)
               vehicleToUpdate.Make = vehicle.Make;
            if (vehicle.Model != null)
               vehicleToUpdate.Model = vehicle.Model;
            if (vehicle.OdometerAtPurchase != null)
               vehicleToUpdate.OdometerAtPurchase = vehicle.OdometerAtPurchase;
            if (vehicle.OdometerAtSale != null)
               vehicleToUpdate.OdometerAtSale = vehicle.OdometerAtSale;
            if (vehicle.PurchasePrice != null)
               vehicleToUpdate.PurchasePrice = vehicle.PurchasePrice;
            if (vehicle.SalePrice != null)
               vehicleToUpdate.SalePrice = vehicle.SalePrice;
            if (vehicle.Vin != null)
               vehicleToUpdate.Vin = vehicle.Vin;
            vehicleToUpdate.Year = vehicle.Year;

            _context.SaveChanges();
         }

         return vehicleToUpdate;
      }

      public Vehicle UpdateVehiclePutEntire(Vehicle vehicle, int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return null;
         }

         Vehicle vehicleToUpdate = GetVehicleById(id);
         if (vehicleToUpdate != null)
         {
            vehicleToUpdate.Color = vehicle.Color;
            vehicleToUpdate.DatePurchased = vehicle.DatePurchased;
            vehicleToUpdate.DateSold = vehicle.DateSold;
            vehicleToUpdate.Make = vehicle.Make;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.OdometerAtPurchase = vehicle.OdometerAtPurchase;
            vehicleToUpdate.OdometerAtSale = vehicle.OdometerAtSale;
            vehicleToUpdate.PurchasePrice = vehicle.PurchasePrice;
            vehicleToUpdate.SalePrice = vehicle.SalePrice;
            vehicleToUpdate.Vin = vehicle.Vin;
            vehicleToUpdate.Year = vehicle.Year;

            _context.SaveChanges();
         }

         return vehicleToUpdate;
      }

      //   D e l e t e

      public bool DeleteVehicle(int id)
      {
         if (_userRepository == null || _userRepository.IsUserLoggedIn() == false)
         {
            return false;
         }

         Vehicle vehicleToDelete = GetVehicleById(id);
         if (vehicleToDelete == null)
         {
            return false;
         }

         try
         {
            _context.Vehicles.Remove(vehicleToDelete);
            _context.SaveChanges();
            return true;
         }
         catch (Exception)
         {
         }

         return false;
      }
   }
}
