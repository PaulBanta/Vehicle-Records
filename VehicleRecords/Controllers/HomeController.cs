using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using VehicleRecords.Data;
using VehicleRecords.Models;

namespace VehicleRecords.Controllers
{
   public class HomeController
      : Controller
   {
      private readonly ILogger<HomeController> _logger;
      private readonly IUserRepository _userRepository;

      public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
      {
         _logger = logger;
         _userRepository = userRepository;
      }

      public IActionResult Index()
      {
         if (_userRepository.IsUserLoggedIn())
         {
            return RedirectToAction("Index", "Vehicle");
         }
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
