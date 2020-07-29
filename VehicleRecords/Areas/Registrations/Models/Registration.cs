using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRecords.Models
{
   [Table("Registration", Schema = "VehRec")]
   public class Registration
   {
      public int Id { get; set; }

      [Column(TypeName = "date")]
      [DataType(DataType.Date)]
      [Required(ErrorMessage = "Date Is Required")]
      [UIHint("date")]
      public DateTime? Date { get; set; }

      [MaxLength(20)] // States in other countries ? ? ?
      public string State { get; set; }

      [Column(TypeName = "decimal(6, 2)")] // $9,999.99
      [Range(0.01, 9999.99, ErrorMessage = "Total Cost Out Of Range")]
      [Required(ErrorMessage = "Total Cost Is Required")]
      [UIHint("number")]
      public double TotalCost { get; set; }

      public int VehicleId { get; set; }

      public Vehicle Vehicle { get; set; }
   }
}
