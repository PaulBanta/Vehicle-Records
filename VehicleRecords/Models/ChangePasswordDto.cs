using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRecords.Models
{
   [NotMapped]
   public class ChangePasswordDto
   {
      // [Display(Name = "Email Address:")]
      [EmailAddress]
      [MaxLength(60)]
      [Required(ErrorMessage = "Email Address is required")]
      public string EmailAddress { get; set; }

      // [Display(Name = "Current Password:")]
      [Required(ErrorMessage = "Current Password is required")]
      [UIHint("password")]
      public string CurrentPassword { get; set; }

      [Compare("NewPasswordVerification")]
      [Display(Name = "New Password")]
      [Required(ErrorMessage = "New Password is required")]
      [UIHint("password")]
      public string NewPassword { get; set; }

      [Display(Name = "Verify New Password")]
      [Required(ErrorMessage = "New Password Verification is required")]
      [UIHint("password")]
      public string NewPasswordVerification { get; set; }
   }
}
