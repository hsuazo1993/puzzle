# Events Management API

This is an API for managing events using ASP.NET Core 6.0 and Entity Framework Core. This application was developed with the Code First approach. This means that the database schema is generated from the entity classes and configurations in the project.
You should look for the ConnectionStrings section in the appsettings file, and update the value of the EventsManagementDB connection string to match your local database configuration. 
This will ensure that the application connects to the right database instance when it starts.

It's also important to note that the application assumes the database named EventsManagement already exists. If the database does not exist, you will need to create it manually before running the application.


In order to add a new migration run the following command:

dotnet ef migrations add My_New_Migration_Name --project DataAccess --startup-project API --output-dir Migrations


Then when your run the application, the new migration will be applied automatically.

## Running the Application

1. Clone the repository to your local machine using `git clone https://github.com/<username>/EventsManagement.git`
2. Navigate to the root directory of the project using the command line.
3. Run `dotnet build` to build the application.
4. Update the connection string in the `appsettings.json` file with your own connection string.
5. Create the database with the name `EventsManagement`.
6. Run `dotnet run` to start the application.

The API can be accessed at `https://localhost:5041` by default.

## Running the Tests

1. Navigate to the root directory of the project using the command line.
2. Run `dotnet test` to run all the tests.

## API Documentation

The API documentation can be found at `https://localhost:5041/swagger` when the application is running.

## License

This project is licensed under the MIT License.