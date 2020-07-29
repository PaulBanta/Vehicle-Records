using System.Linq;
using VehicleRecords.Models;

namespace VehicleRecords.Data
{
   public interface IUserRepository
   {
      //   C r e a t e

      public bool CreateUser(string emailAddress);

      //   R e a d

      public IQueryable<User> GetAllUsers(); // Temp For Debugging ? ? ?

      public string GetLoggedInUserEmail();

      public int? GetLoggedInUserId();

      public bool IsUserLoggedIn();

      public bool UserLoggedInWithTempPassword();

      //   U p d a t e

      public bool ChangePassword(string emailAddress, string oldPassword, string newPassword);

      public bool Login(string emailAddress, string password);

      public void Logout();

      public bool ResetPassword(string emailAddress);

      //   D e l e t e
   }
}
