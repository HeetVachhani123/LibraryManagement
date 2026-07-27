# 📚 Library Management System (LMSystem)

![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%2010.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

A comprehensive, production-ready web application built with **ASP.NET Core MVC** and **C#** to manage library inventory, track student borrowing records, and streamline librarian administrative tasks. 

Recently revamped with a stunning **Premium UI/UX**, featuring dark/light mode, glassmorphism, and smooth micro-interactions.

---

## ✨ Key Features

- 🎨 **Premium UI/UX:** Complete front-end overhaul with a sleek, minimalist aesthetic, featuring a dynamic Dark/Light mode toggle, floating glassmorphic navbars, and interactive hover animations.
- 📖 **Extensive Inventory Management:** Full CRUD operations for Books, Magazines, and Newspapers.
- 👥 **User Roles:** Distinct dashboards and workflows for **Students** and **Librarians**.
- 🔄 **Real-Time Borrowing System:** Track who borrowed what, when it's due, and current availability status using seamless SQL integrations.
- 🛡️ **Secure Authentication:** Cookie-based authentication and role authorization.
- ⚡ **High-Performance Data Access:** Utilizes both **Entity Framework Core (EF Core)** for robust entity tracking and **Raw ADO.NET** for hyper-fast, direct database queries where performance is critical.

---

## 🛠️ Tech Stack & Architecture

- **Backend:** ASP.NET Core MVC (.NET 8/10)
- **Frontend:** HTML5, Razor Views (`.cshtml`), Bootstrap 5.3, Custom Premium CSS
- **Database:** Microsoft SQL Server (LocalDB / Docker compatible)

---

## 🚀 Getting Started (Windows)

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (Installs with Visual Studio)

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/HeetVachhani123/LibraryManagement.git
   cd LibraryManagement/LIbrary/LMSystem
   ```

2. **Initialize the Database:**
   ```bash
   dotnet run -- --initdb
   ```

3. **Start the Application:**
   ```bash
   dotnet run
   ```
