README
==========================================

How to compile the sourcecode
-----------------------------------
To compile the sourcecode, make sure you place the files mentioned in \Externals\FilesToPlaceHere.txt into 
the folder \Externals before compiling. It can be DevDeploy(4).bat fails after compilation. Adjust the paths
in those bat files or remove the post-build event. If you copied LINQPad.exe v4.40 or higher in the Externals
folder, you have to change the target framework to .NET 4 for the driver project to compile the code. 


Requirements to compile the code:
-----------------------------------
.NET 4.5.2 SDK or higher. Recommented: VS.NET 2015 or higher or Rider 2017.x or higher


How to distribute the driver
------------------------------------
To distribute the driver, zip the dll and header.xml into a zip file and rename the extension to .lpx. 


Executing SQL
------------------
The driver isn't designed to be used to execute SQL against a database, though it will work if the following
is true:
- You specify a connection string in the connection dialog
- In the connection string specified you connect to a SQL Server service.


Executing QuerySpec or Low-level API queries
----------------------------------------------
Set the 'language' combo box in the query pane in LINQpad to 'C# Statements' or 'VB.NET statements'.
Specify the query as-is. To see results, use the Dump(); extension method by appending it to the 
results. 

For Adapter, obtain the adapter from the property 'AdapterToUse'. See the example below:

// Queryspec:
var qf = new QueryFactory();
var results = this.AdapterToUse.FetchQuery(qf.Customer);
results.Dump();

// Low level api:
var managers = new EntityCollection<ManagerEntity>();
this.AdapterToUse.FetchEntityCollection(managers, null);
managers.Dump();
