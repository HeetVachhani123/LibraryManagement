# Library Management System (LMSystem)

Welcome to the Library Management System! This is a comprehensive web application built with **ASP.NET Core MVC** and **C#**. It serves as a unified system to manage library inventory, staff, and users.

## 🛠️ Tech Stack & Architecture

- **Backend Framework:** ASP.NET Core MVC (.NET 8/7)
- **Frontend Design:** Razor Views (`.cshtml`), Bootstrap 5.3.0, Vanilla CSS
- **Database:** Microsoft SQL Server
- **Data Access Patterns:**
  - **Entity Framework Core (EF Core):** Used for managing `Books`, `Publications` (Magazines, Newspapers), and `BorrowRecords`.
  - **Raw ADO.NET (`SqlConnection` & `SqlCommand`):** Used for high-performance direct database queries in the `Students`, `Librarians`, `Dashboard`, and `Login` modules.

## 🚀 Key Features

1. **Dashboard:** Provides at-a-glance metrics (Total Students, Books, Librarians, Borrowings, and Publications).
2. **Book & Publication Management:** Full CRUD operations for Books, Newspapers, and Magazines with integrated pagination and dynamic search functionality.
3. **Staff & User Management:** Manage Librarians and Students using ADO.NET windowed queries (`OFFSET-FETCH`) for optimal pagination.
4. **Borrow/Return System:** Track when items are checked out and returned.
5. **Authentication:** Secure cookie-based login system for administrators.

---

## 💻 How to Run Locally (Windows)

Since this project uses SQL Server, running it on a Windows machine is very straightforward using **SQL Server LocalDB** (which is installed alongside Visual Studio).

### Step 1: Update the Database Connection String
By default, the project might be configured to run on a Mac using a Docker container. You will need to change the connection string to use Windows LocalDB.

1. Open `appsettings.json`.
2. Locate the `ConnectionStrings` section.
3. Update `DefaultConnection` to the following:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LMS;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

### Step 2: Initialize the Database and Seed Data
This project includes a built-in `--initdb` flag that automatically creates the database schema, drops existing tables if necessary, and populates the database with initial seed data.

1. Open your terminal or Command Prompt in the project's root directory (`LMSystem`).
2. Run the following command:
   ```bash
   dotnet run -- --initdb
   ```
   *You should see an output saying "Database initialized successfully!"*

### Step 3: Run the Application
Once the database is set up, you can start the application normally.

1. In the same terminal, run:
   ```bash
   dotnet run
   ```
2. Open your web browser and navigate to the URL provided in the console (usually `http://localhost:5xxx`).

### Step 4: Login Credentials
To access the secure parts of the application (like adding or editing items), you can log in using the seeded administrator account:
- **Username:** `admin`
- **Password:** `12345`

Enjoy exploring the Library Management System!
