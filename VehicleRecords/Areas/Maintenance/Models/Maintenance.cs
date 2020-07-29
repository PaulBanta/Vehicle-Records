using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRecords.Models
{
   [Table("Maintenance", Schema = "VehRec")]
   public class Maintenance // & Repairs
   {
      public int Id { get; set; }

      [Column(TypeName = "date")]
      [DataType(DataType.Date)]
      [Required(ErrorMessage = "Date Is Required")]
      [UIHint("date")]
      public DateTime? Date { get; set; }

      [MaxLength(40)]
      [Required(ErrorMessage = "Performed By Is Required")]
      public string PerformedBy { get; set; }

      [MaxLength(40)]
      [Required(ErrorMessage = "Brief Description Of Work Is Required")]
      public string BriefDescriptionOfWork { get; set; }

      [MaxLength(1000)]
      public string FullDescriptionOfWork { get; set; }

      [Range(0, 1000000, ErrorMessage = "Odometer Out Of Range")]
      [UIHint("number")]
      public int? Odometer { get; set; }

      [Column(TypeName = "decimal(7, 2)")] // $99,999.99
      [Range(0.01, 99999.99, ErrorMessage = "Total Cost Out Of Range")]
      [Required(ErrorMessage = "Total Cost Is Required")]
      [UIHint("number")]
      public double TotalCost { get; set; }

      public int VehicleId { get; set; }

      public Vehicle Vehicle { get; set; }
   }
}
