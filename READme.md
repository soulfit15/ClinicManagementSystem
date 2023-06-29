# ClinicDataManagement
ClinicDataManagement system is a clinic system for educational purposes made by me, as a physiotherapist and 
an avid C# programmer.

All of the app is developed in visual studio with the try to implement most of the features to help the user,
as a doctor to manage his clinic

Th First Implementation of the System is a patient data manager that acts as a history sheet with patient name, age and address
stored into a [PostgreSQL](https://github.com/postgres/postgres) database and managed by [Npgsql](https://github.com/npgsql/npgsql) and [Dapper](https://github.com/DapperLib/Dapper).

the patient data manager has a datagrid connected to database by *NpgsqlDataAdapter*, I am unable to edit the database record with the datagrid
so I use Buttons to do the job

The "Add Patient" button works only when all the 3 textboxes have correct data types and have input data.

The "Edit Patient" button works with patient ID and require all three text boxes to work.

The "Delete" button acts only on id, so insert id you want to delete and push the button and the record with the id will be deleted.

## Requirements
1. .NET SDK installed (latest perferred)
2. PostgreSQL and make a new Database with table patients and 4 columns (id "serial", name"varchar",age"int",address"varchar").

## Build Steps
1. make sure to configure the connection string on the MainWindow.xaml.cs, check SQL queries with your database table and columns.

  ```
public NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Username=<username>;Password=<password>;Database=<dbname>;");

```
```
where <username> is postgres server user name (default is postgres)

<password> is postgres server password

<dbname> is database name
```
3. in the folder that contains ,csproj file open new command prompt and enter the following code

```
dotnet run
```
