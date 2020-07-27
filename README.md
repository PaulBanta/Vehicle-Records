# [Vehicle-Records](https://VehicleRecords.AzureWebSites.Net)

A C# ASP.NET Core MVC 3.1 Web Application

### Purpose

**Vehicle Records** is an application for users to keep track of all of their vehicles as well as the each vehicle's records.
These records include Fuel Purchases (Fillups), Maintenance & Repairs, Insurance Payments, Registration, and any Accident Information : - (.

### Key Features

- Complete Register / Login / Logout / Change Password / Reset Password functionality
  - Passwords AND EMail Addresses (UserNames) are deeply encrypted
  - Minimal personal information is required and stored
- Full CRUD for User Vehicles
- Full CRUD for Vehicle Fillups
  - Automatically computes Fillup Statistics
    - Miles Per Day
    - Miles Per Gallon
    - Price Per Day
    - Price Per Gallon
    - Price Per Mile
- **Coming Soon**
  - Full CRUD for Maintenance & Repairs
  - Full CRUD for Insurance Payments
  - Full CRUD for Registration

### Backlog / Stretch Goals

- Full CRUD for Maintenance / Repairs (currently Add and Read only)
- Full CRUD for Insurance
- Full CRUD for Registration
- Full CRUD for Accidents
- Full CRUD for Income Generation (Uber, Lyft, Etc.)
- Add ability to download Fillup information in Excel CSV format
- Change Id fields to use GUIDs
- Compute and display Cost Per Month & Cost Per Year
- Add graphs of Gas Mileage, Fuel Prices, etc.
- When a new User registers, store the EMail Address & Temp Password in Session only
- Divide Application into Areas in order to logically isolate functionality

### Technologies Used

- C#
- ASP.NET Core MVC 3.1
  - C# Models
    - Repositories, LInQ
    - Session
  - Razor Views
    - Partial Pages
    - View Components (eventually removed in favor of Razor Dependency Injection)
  - C# Controllers
    - Dependency Injection / Inversion Of Control
- Entity Framework to SQL Server database
  - DbContext & DbSet
- Azure
- HTML 5
- CSS 3
- Bootstrap 4
- HTTP
  - Get
  - Post
- Visual Studio 2019
- Git / GitHub / BitBucket
