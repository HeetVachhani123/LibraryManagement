# Library Management System

This is a Library Management System web application built using ASP.NET Core MVC and C#. It allows for managing books, students, and librarians.

## Features
- Manage Books, Newspapers, and Magazines
- Dashboard for Students and Librarians
- Borrow and return functionality
- Clean user interface with Light and Dark modes
- SQL Server database integration

## Technologies Used
- ASP.NET Core MVC (.NET 8/10)
- C#
- Entity Framework Core & ADO.NET
- HTML, CSS, Bootstrap
- Microsoft SQL Server LocalDB

## How to Run the Project
1. Make sure you have the .NET SDK installed on your computer.
2. Open the terminal and navigate to the `LIbrary/LMSystem` folder.
3. Run the following command to setup the database:
   `dotnet run -- --initdb`
4. Start the application by running:
   `dotnet run`
5. Open your web browser and go to `http://localhost:5000`

## Structure
- Controllers: Contains the logic for the pages.
- Models: Database schemas and view models.
- Views: The HTML pages.
