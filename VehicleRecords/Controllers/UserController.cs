using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   public class UserController : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly IUserRepository _repository;

      //   C o n s t r u c t o r s

      public UserController(IUserRepository repository)
      {
         _repository = repository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Register()
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         return View(new User());
      }

      [HttpPost]
      public IActionResult Register(User user)
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         if (ModelState.IsValid)
         {
            bool succeeded = _repository.CreateUser(user.EmailAddress);
            if (succeeded)
               return View("RegisterSuccess", user);

            ModelState.AddModelError("", "This Email Address is already registered");
         }

         return View(user);
      }

      //   R e a d

      //   U p d a t e

      [HttpGet]
      public IActionResult ChangePassword()
      {
         if (!_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         return View(new ChangePasswordDto { EmailAddress = _repository.GetLoggedInUserEmail() });
      }

      [HttpPost]
      public IActionResult ChangePassword(ChangePasswordDto dto)
      {
         if (!_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         if (ModelState.IsValid)
         {
            bool succeeded = _repository.ChangePassword(dto.EmailAddress, dto.CurrentPassword, dto.NewPassword);
            if (succeeded)
               return View("ChangePasswordSuccess", new User { EmailAddress = dto.EmailAddress });

            ModelState.AddModelError("", "Current Password is wrong");
         }

         return View(dto);
      }

      [HttpGet]
      public IActionResult Login()
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         return View();
      }

      [HttpPost]
      public IActionResult Login(User user)
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         if (ModelState.IsValid)
         {
            if (_repository.Login(user.EmailAddress, user.Password))
            {
               if (_repository.UserLoggedInWithTempPassword())
                  return RedirectToAction("ChangePassword");

               return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Wrong Email Address and/or Password");
         }

         return View(user);
      }

      public IActionResult Logout()
      {
         _repository.Logout();
         return RedirectToAction("Index", "Home");
      }

      [HttpGet]
      public IActionResult ResetPassword()
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         return View();
      }

      [HttpPost]
      public IActionResult ResetPassword(User user)
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Home");

         if (ModelState.IsValid)
         {
            bool succeeded = _repository.ResetPassword(user.EmailAddress);
            if (succeeded == true)
               return View("ResetPasswordSuccess", user);

            ModelState.AddModelError("", "This Email Address is not registered");
         }

         return View(user);
      }

      //   D e l e t e
   }
}
