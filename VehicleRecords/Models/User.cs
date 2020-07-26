using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRecords.Models
{
   [Table("User", Schema = "VehRec")]
   public class User
   {
      public int Id { get; set; }

      // [Display(Name = "Email Address:")]
      [EmailAddress]
      [MaxLength(128)]
      [Required(ErrorMessage = "Email Address is required")]
      public string EmailAddress { get; set; }

      // [Display(Name = "Password:")]
      [MaxLength(128)]
      [Required(ErrorMessage = "Password is required")]
      [UIHint("password")]
      public string Password { get; set; }
   }
}
