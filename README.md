# BondoraAssignement
REQUIRED : Visual Studio 2019 or 2017 with Asp.net and SQL

First of all, clone the project to your computer.
Then, create a directory named "App_Data" in "Sources/BondoraAssignment/".

Next, open Visual Studio 2019.

Open the Server Explorer (View > Server Explorer).

Add a new connection.
In "Data source", select  Local File Database Microsoft SQL Server (SqlClient)
In "Server Name", browse to the "App_Data" folder that we have created previously and select this folder.
Name the Database "BondoraAssignementDB".
In the end we should have a connection string that look like this : "PATH_TO_GIT_FOLDER_\Sources\BondoraAssignment\App_Data\BondoraAssignementDB"

Click Ok, Visual studio will ask if you want to create the database. Say yes.
Now the Database is created.

Next, in the server explorer, right click on "BondoraAssignementDB" and select "New Query".
It will open a new tab named "SQLQuery1.sql". Open the file "/Ressources/Script DB Construction.sql", select all and copy.
Return to the tab "SQLQuery1.sql" and paste inside it.
Press execute on top of the tab.
The database is now set.

Be sure that the project BondoraAssignement is the starting project.

Make sure that Port: '44354' is not occupied by any other process.

Clean and execute the project.

And Voila you can test it ! 

__________________________

TODO : 
- Loggin
- Docker
- Better error handling
- Mock test for DB

