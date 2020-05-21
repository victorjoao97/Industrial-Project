#  Energy and Material Balance Module


Energy and Material Balance Module is a browser-based WPF (XBAP) application created for SEICBalance database configuration:

  - See the list of all Resources
  - See all BGroups in the tree view
  - See valid disbalance for each BGroup
  - Add new BGroup
  - Delete existing BGroup
  - Update existing BGroup

### Used Technology 

* C#
* Visual Studio
* APS.NET Core MVC
* Razor Pages
* Entity Framework 6
* Bootstrap (css framework)
* JQuery (js framework)
* MSSQL Server


### Run application

To run application:
1. Import "SEICBalance.bak" file to database in MSSQL Server
2. Open EnergyAndMaterialBalanceModule in Visual Studio
3. Open "appsettings.json"
5. Change "SEICBalanceConnection" in "ConnectionStrings" to your actual MSSQL data
```
"ConnectionStrings": {
    "SEICBalanceConnection": "Server=localhost;Database=SEICBalance;Trusted_Connection=false;User ID=sa;Password=Password;"
  }
```
13. Build and run EnergyAndMaterialBalanceModule

