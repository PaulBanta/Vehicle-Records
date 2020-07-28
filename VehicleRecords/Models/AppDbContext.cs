using Microsoft.EntityFrameworkCore;
using VehicleRecords.Areas.Insurance.Models;

namespace VehicleRecords.Models
{
   public class AppDbContext
      : DbContext
   {
      //   F i e l d s   &   P r o p e r t i e s

      public DbSet<Fillup> Fillups { get; set; }

      public DbSet<Insurance> Insurance { get; set; }

      public DbSet<MaintenanceRepair> MaintenanceRepairs { get; set; }

      public DbSet<User> Users { get; set; }

      public DbSet<Vehicle> Vehicles { get; set; }

      //   C o n s t r u c t o r s

      public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
      {
      }

      //   M e t h o d s

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         //   U s e r

         modelBuilder.Entity<User>()
                     .HasIndex(u => u.EmailAddress)
                     .IsUnique();

         //   F i l l u p

         modelBuilder.Entity<Fillup>()
            .Property(f => f.DaysSinceLastFillup)
            .HasDefaultValue(1);

         modelBuilder.Entity<Fillup>()
            .Property(f => f.MilesPerDay)
            .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [TripOdometer] / [DaysSinceLastFillup], [TripOdometer] )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.MilesPerGallon)
            .HasComputedColumnSql("iif( [Gallons] > 0, [TripOdometer] / [Gallons], 999.9 )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.PricePerDay)
            .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [TotalCost] / [DaysSinceLastFillup], [TotalCost] )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.PricePerGallon)
            .HasComputedColumnSql("iif( [Gallons] > 0, [TotalCost] / [Gallons], 999.9 )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.PricePerMile)
            .HasComputedColumnSql("iif( [TripOdometer] > 0, [TotalCost] / [TripOdometer], 999.9 )");

         //   I n s u r a n c e

         modelBuilder.Entity<Insurance>()
            .Property(i => i.CostPerMonth)
            .HasComputedColumnSql("iif( [NumberOfMonths] > 0, [TotalCost] / [NumberOfMonths], 999.9 )");

         //   U s e r s

         modelBuilder.Entity<User>().HasData(new User
         {
            Id = 1,
            EmailAddress = "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD",
            Password = "0389807F1DB834156EDD82E92D8EBCED3834A457F52B33072B697A9F12C65A9F0EE0D05C0555FA1EDE493E7B168AAA5434DFDE3F4B18E6BD47EFE9E84E171FF6"
         }); // PBanta101@GMail.Com

         modelBuilder.Entity<User>().HasData(new User
         {
            Id = 2,
            EmailAddress = "4F988DDAFEE8760D1B56532E2C1C356EAC82DF48EA37F47E86F93230032236C64A0183C9C8670DC9D68D1F5F52E1A6F474FB88ACCA637DD0592894AB442EAA12",
            Password = "211B75AF9B9A156488BC8DC2D576F2E0DCF52A79C077F7C250C74683CED51350E245C0E9290EEBD5B06208FF644F964C4E74604460F29005D9A9FE982348BFA7"
         }); // PBanta@Example.Com

         //   V e h i c l e s

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 1,
            Year = 1995,
            Make = "Saturn",
            Model = "SW2",
            Color = "Gold",
            DatePurchased = new System.DateTime(1995, 04, 10),
            PurchasePrice = 12500,
            OdometerAtPurchase = 10,
            DateSold = new System.DateTime(2010, 06, 15),
            SalePrice = 1000,
            OdometerAtSale = 234000,
            UserId = 1
         });

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 2,
            Year = 2001,
            Make = "Honda",
            Model = "CR-V",
            Color = "Black",
            UserId = 1
         });

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 3,
            Year = 2007,
            Make = "Chevrolet",
            Model = "Malibu",
            Color = "Gold",
            UserId = 1
         });

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 4,
            Year = 2008,
            Make = "Subaru",
            Model = "Forester",
            Color = "Gold",
            UserId = 1
         });

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 5,
            Year = 2012,
            Make = "Mazda",
            Model = "3",
            Color = "Blue",
            UserId = 1
         });

         //   F i l l u p s

         modelBuilder.Entity<Fillup>().HasData(new Fillup
         {
            Id = 1,
            Date = new System.DateTime(2020, 7, 1),
            DaysSinceLastFillup = 0,
            Gallons = 10.001,
            Odometer = 300,
            TotalCost = 24.99,
            TripOdometer = 300.1,
            VehicleId = 1
         });

         modelBuilder.Entity<Fillup>().HasData(new Fillup
         {
            Id = 2,
            Date = new System.DateTime(2020, 7, 7),
            DaysSinceLastFillup = 6,
            Gallons = 9.001,
            Odometer = 600,
            TotalCost = 22.00,
            TripOdometer = 300.1,
            VehicleId = 1
         });

         modelBuilder.Entity<Fillup>().HasData(new Fillup
         {
            Id = 3,
            Date = new System.DateTime(2020, 7, 16),
            DaysSinceLastFillup = 9,
            Gallons = 12.001,
            Odometer = 900,
            TotalCost = 22.00,
            TripOdometer = 300.1,
            VehicleId = 1
         });

         modelBuilder.Entity<Fillup>().HasData(new Fillup
         {
            Id = 4,
            Date = new System.DateTime(2020, 7, 22),
            DaysSinceLastFillup = 6,
            Gallons = 12.001,
            Odometer = 1350,
            TotalCost = 22.00,
            TripOdometer = 450.1,
            VehicleId = 1
         });

         //   M a i n t e n a n c e   &   R e p a i r s

         modelBuilder.Entity<MaintenanceRepair>().HasData(new MaintenanceRepair
         {
            Id = 1,
            Date = new System.DateTime(2020, 7, 2),
            BriefDescriptionOfWork = "Oil Change",
            PerformedBy = "Self",
            Odometer = 200000,
            TotalCost = 22.49,
            VehicleId = 1
         });

         modelBuilder.Entity<MaintenanceRepair>().HasData(new MaintenanceRepair
         {
            Id = 2,
            Date = new System.DateTime(2020, 7, 18),
            BriefDescriptionOfWork = "Replace Clutch",
            FullDescriptionOfWork = "Parts: $150\nLabor: $245\nWarranty: 6 Months / 6,000 Miles",
            PerformedBy = "Woodmoor Conoco",
            Odometer = 200150,
            TotalCost = 395,
            VehicleId = 1
         });
      }
   }
}
