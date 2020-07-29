using Microsoft.AspNetCore.Http;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VehicleRecords.Models;

namespace VehicleRecords.Data
{
   public class EfUserRepository
      : IUserRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      // Class Variables
      private static SHA512 _hash = SHA512.Create();
      private static Random _random = new Random();

      // Instance Variables
      private readonly AppDbContext _context;
      // private readonly IHttpContextAccessor _httpContextAccessor;
      private readonly ISession _session;
      private bool _userLoggedInWithTempPassword;

      //   C o n s t r u c t o r s

      public EfUserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
      {
         _context = context;
         // _httpContextAccessor = httpContextAccessor;
         _session = httpContextAccessor.HttpContext.Session;
      }

      //   M e t h o d s

      //   C r e a t e

      public bool CreateUser(string emailAddress)
      {
         string lcEmailAddress = emailAddress.ToLower();
         string hashedEmailAddress = Hash(lcEmailAddress);

         if (_context.Users.Any(u => u.EmailAddress == hashedEmailAddress))
            return false;

         int randomLength = _random.Next(4, 6) * 2 + 1;
         string randomPassword = RandomPassword(randomLength);
         User newUser = new User
         {
            EmailAddress = hashedEmailAddress,
            Password = Hash(lcEmailAddress, randomPassword),
         };
         _context.Users.Add(newUser);
         _context.SaveChanges();

         _ = EmailPassword("Vehicle Records Account Created", emailAddress, randomPassword,
            "After you login, click on your email address at the top of the page in order to change your password.");

         return true;
      }

      //   R e a d

      public IQueryable<User> GetAllUsers() // Temp For Debugging ? ? ?
      {
         return _context.Users.OrderBy(u => u.Id);
      }

      public string GetLoggedInUserEmail()
      {
         return _session.GetString("_UserEmail");
      }

      public int? GetLoggedInUserId()
      {
         return _session.GetInt32("_UserId");
      }

      public bool IsUserLoggedIn()
      {
         return GetLoggedInUserId() != null;
      }

      public bool UserLoggedInWithTempPassword()
      {
         return _userLoggedInWithTempPassword;
      }


      //   U p d a t e

      public bool ChangePassword(string emailAddress, string oldPassword, string newPassword)
      {
         string lcEmailAddress = emailAddress.ToLower();

         User existingUser = GetUser(lcEmailAddress, oldPassword);
         if (existingUser == null)
            return false;

         existingUser.Password = Hash(lcEmailAddress, newPassword);
         _context.SaveChanges();

         _session.Remove("_ResetPassword_" + lcEmailAddress);
         _session.Remove("_ResetTimestamp_" + lcEmailAddress);

         return true;
      }

      public bool Login(string emailAddress, string password)
      {
         string lcEmailAddress = emailAddress.ToLower();

         User existingUser = GetUser(lcEmailAddress, password);
         if (existingUser == null)
            return false;

         _session.SetString("_UserEmail", emailAddress);
         _session.SetInt32("_UserId", existingUser.Id);

         return true;
      }

      public void Logout()
      {
         _session.Remove("_UserEmail");
         _session.Remove("_UserId");
      }

      public bool ResetPassword(string emailAddress)
      {
         string lcEmailAddress = emailAddress.ToLower();

         User existingUser = GetUser(lcEmailAddress);
         if (existingUser == null)
            return false;

         long timestampNowSeconds = GetTimestampNowSeconds();

         string sessionResetTimestamp = _session.GetString("_ResetTimestamp_" + lcEmailAddress);
         if (sessionResetTimestamp != null)
         {
            if (timestampNowSeconds - long.Parse(sessionResetTimestamp) <= 15 * 60)
            {
               return true;
            }
         }

         int randomLength = _random.Next(4, 6) * 2 + 1;
         string randomPassword = RandomPassword(randomLength);
         _session.SetString("_ResetPassword_" + lcEmailAddress, Hash(lcEmailAddress, randomPassword));
         _session.SetString("_ResetTimestamp_" + lcEmailAddress, timestampNowSeconds.ToString());

         _ = EmailPassword("Vehicle Records Password Reset", emailAddress, randomPassword,
            "This password is temporary.<br />It will expire in 15 minutes.<br />Your old password will still work if you remember it.");

         return true;
      }

      //   D e l e t e

      //   P r i v a t e   M e t h o d s

      private static async Task EmailPassword(string subject, string emailAddress, string password, string message = "")
      {
         var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
         var client = new SendGridClient(apiKey);
         var from = new EmailAddress("DazzleFlashInc@GMail.Com", "Vehicle Records");
         var to = new EmailAddress(emailAddress);
         var plainTextContent = $"Vehicle Records Account Information\n\nEmail Address: {emailAddress}\nPassword: {password}\n\n{message.Replace("<br />", "\n")}";
         var htmlContent =
            "<div style=\"font-size: 120%\">"
            + "<table>"
            + "<caption> Vehicle Records Account Information </caption>"
            + "<tr>"
            + "<th align=\"right\"> Email Address: </th>"
            + "<td align=\"left\"> " + emailAddress + " </td>"
            + "</tr>"
            + "<tr>"
            + "<th align=\"right\"> Password: </th>"
            + "<td align=\"left\"> " + password + " </td>"
            + "</tr>"
            + "</table>"
            + "<p> " + message + " </p>"
            + "</div>";
         var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
         _ = await client.SendEmailAsync(msg);
      }

      private long GetTimestampNowSeconds()
      {
         return DateTime.Now.Ticks / 10000000;
      }

      private User GetUser(string lcEmailAddress)
      {
         return _context.Users.FirstOrDefault(u => u.EmailAddress == Hash(lcEmailAddress));
      }

      private User GetUser(string lcEmailAddress, string password)
      {
         User existingUser = GetUser(lcEmailAddress);

         if (existingUser != null)
         {
            string hashedPassword = Hash(lcEmailAddress, password);
            if (hashedPassword == existingUser.Password)
            {
               _userLoggedInWithTempPassword = false;
               return existingUser;
            }

            string sessionResetTimestamp = _session.GetString("_ResetTimestamp_" + lcEmailAddress);
            if (sessionResetTimestamp != null)
            {
               long elapsedTimeSeconds = GetTimestampNowSeconds() - long.Parse(sessionResetTimestamp);
               if (elapsedTimeSeconds <= 15 * 60)
               {
                  string tempSessionPassword = _session.GetString("_ResetPassword_" + lcEmailAddress);
                  if (tempSessionPassword != null && tempSessionPassword == hashedPassword)
                  {
                     _userLoggedInWithTempPassword = true;
                     return existingUser;
                  }
               }
            }
         }

         return null;
      }

      private static string Hash(string lcUsername)
      {
         byte[] usernameBytes = Encoding.ASCII.GetBytes(lcUsername);
         byte[] hashedBytes = _hash.ComputeHash(usernameBytes);
         return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
      }

      private static string Hash(string lcUsername, string password)
      {
         byte[] usernameBytes = Encoding.ASCII.GetBytes(lcUsername);
         byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
         int length = Math.Max(usernameBytes.Length, passwordBytes.Length);
         byte[] saltedBytes = new byte[length];
         for (int b = 0; b < length; b++)
         {
            saltedBytes[b] = (byte)(usernameBytes[b % usernameBytes.Length] ^ passwordBytes[b % passwordBytes.Length]);
         }
         byte[] hashedBytes = _hash.ComputeHash(saltedBytes);
         return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
      }

      private static string RandomPassword(int length)
      {
         string result = "";
         for (int i = 0; i < length; i++)
            result += (char)_random.Next(33, 126);
         return result;
      }
   }
}
