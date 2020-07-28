using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRecords.Models;

namespace VehicleRecords.Areas.Insurance.Models
{
   [Table("Insurance", Schema = "VehRec")]
   public class Insurance
   {
      public int Id { get; set; }

      [Column(TypeName = "date")]
      [DataType(DataType.Date)]
      [Required(ErrorMessage = "Date Is Required")]
      [UIHint("date")]
      public DateTime? Date { get; set; }

      [Range(1, 24, ErrorMessage = "Number Of Months Out Of Range")]
      [Required(ErrorMessage = "Number Of Months Is Required")]
      [UIHint("number")]
      public int NumberOfMonths { get; set; }

      [MaxLength(40)]
      [Required(ErrorMessage = "Company Is Required")]
      public string Company { get; set; }

      [MaxLength(40)]
      public string PolicyNumber { get; set; }

      [MaxLength(40)]
      public string Coverage { get; set; }

      [Column(TypeName = "decimal(7, 2)")] // $99,999.99
      [Range(0.01, 99999.99, ErrorMessage = "Total Cost Out Of Range")]
      [Required(ErrorMessage = "Total Cost Is Required")]
      [UIHint("number")]
      public double TotalCost { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(7, 2)")] // $99,999.99
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double CostPerMonth { get; set; }

      public int VehicleId { get; set; }

      public Vehicle Vehicle { get; set; }
   }
}
