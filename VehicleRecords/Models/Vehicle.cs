using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRecords.Models
{
   [Table("Vehicle", Schema = "VehRec")]
   public class Vehicle
   {
      public int Id { get; set; }

      [Range(1900, Int32.MaxValue, ErrorMessage = "Year Must Be At Least 1900")]
      [Required(ErrorMessage = "Year Is Required")]
      [UIHint("number")]
      public int Year { get; set; }

      [MaxLength(40)]
      [Required(ErrorMessage = "Make Is Required")]
      public string Make { get; set; }

      [MaxLength(40)]
      [Required(ErrorMessage = "Model Is Required")]
      public string Model { get; set; }

      [MaxLength(20)]
      public string Color { get; set; }

      [MaxLength(20)]
      public string Vin { get; set; }

      [Column(TypeName = "date")]
      [DataType(DataType.Date)]
      [UIHint("date")]
      public DateTime? DatePurchased { get; set; }

      [Column(TypeName = "money")]
      [Range(0, Int32.MaxValue)]
      [UIHint("number")]
      public double? PurchasePrice { get; set; }

      [Range(0, Int32.MaxValue)]
      [UIHint("number")]
      public int? OdometerAtPurchase { get; set; }

      [Column(TypeName = "date")]
      [DataType(DataType.Date)]
      [UIHint("date")]
      public DateTime? DateSold { get; set; }

      [Column(TypeName = "money")]
      [UIHint("number")]
      public double? SalePrice { get; set; }

      [Range(0, Int32.MaxValue)]
      [UIHint("number")]
      public int? OdometerAtSale { get; set; }

      public int UserId { get; set; }

      public User User { get; set; }

      public IEnumerable<Fillup> Fillups { get; set; }

      public IEnumerable<MaintenanceRepair> MaintenanceRepairs { get; set; }
   }
}
