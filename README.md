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
* WPF application
* XAML 
* Entity Framework 6
* Prism
* Autofac
* MSSQL Server


### Run application

The project consists of three packages.

The main package: 
* BCG_UI (views and viewmodels)

Additional packages:
* Models (models)
* Data Access (connection with SEICBalance database)

To run application:
1. Import "SEICBalance.bak" file to database in MSSQL Server
2. Open "BCG.sln" file in Visual Studio
3. Open BCG_UI package
4. Open "app.config"
5. Change "source" in "connectionString" to your actual MSSQL Server user name 
```
<connectionStrings>
	<add name="SEICBalance" connectionString="data source=YOURDATA;initial catalog=SEICBalance;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
6. Open Models package
7. Open "app.config"
8. Change "source" in "connectionString" to your actual MSSQL Server user name 
```
<connectionStrings>
	<add name="SEICBalance" connectionString="data source=YOURDATA;initial catalog=SEICBalance;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
9. Open DataAccess package
10. Open "app.config"
11. Change "source" in "connectionString" to your actual MSSQL Server user name 
```
<connectionStrings>
	<add name="SEICBalance" connectionString="data source=YOURDATA;initial catalog=SEICBalance;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
12. Open BCG_UI package
13. Compile and run BCG_UI 
14. Application will open in IE browser

### Task Complition Guide
 -Implement each task in separate branch.
Branch naming starts with the number of tasks in Jira and continued with brief naming, referring to the name of task. Example: sip-255-fr-1.7-repo-four-BGroups for task:  SIP-255 FR 1.7 - Create repository for “BGroups”.

 - After work is done create pull request into dev
 - Name pull request similar to the task name. 
Example: FR 1.7 - Create repository for “BGroups”
 - Add the reference to task in Jira into description of request. Also In the description of the task in Jira reference to pull request.

 - Write request to review in telegram and add ref to pull request.

 - Until request will be approved by Architect and some one from Devs it cant be merged

