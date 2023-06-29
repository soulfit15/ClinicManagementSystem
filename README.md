# ClinicManagementSystem
Clinic Management system is a bunch of programs made for educational purposes using visual studio 2022. Currently working on the first program which is a patient data manager with the SQL database using dapper and npgsql. The application hosts a datagrid bound to the DB by ```NpgsqlDataAdapter``` and dataset filling. The datagrid only shows the data and all operations are done by buttons.  

## Tech used/required to install:
1. Visual Studio 2022
2. [PostgreSQL](https://github.com/postgres/postgres).
3. [Npgsql](https://github.com/npgsql/npgsql).
4. [Dapper](https://github.com/DapperLib/Dapper).

## How to build
1. install PostgreSQL and create a new DB with table patients, 5 columns {id"serial", name"varchar", age"int", address"varchar", gender"char"}.
2. make sure that dependencies are installed as Dapper and Npgsql (if not, open the project in visual studio 2022, right click on dependencies and manage nuget packages and install both Npgsql and Dapper).
3. modify the connection string in

    ``` NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Username=<username>;Password=<password>;Database=<databasename>");```
   
where username is postgreSQL server username

password is psql server password, databasename is the name of the database
4. Build and run

## What I couldn't do
I wanted the datagrid to control the entries to the SQL Database, but I was Sadly unable, but I will keep tinkering to get to it 
