CREATE DATABASE LMS;
GO

USE LMS;
GO

DROP TABLE IF EXISTS BorrowRecords13;
DROP TABLE IF EXISTS Books13;
DROP TABLE IF EXISTS Publications;
DROP TABLE IF EXISTS Students;
DROP TABLE IF EXISTS Librarians;
DROP TABLE IF EXISTS logintab;
GO

-- 1. Login Module Table (Raw ADO.NET)
CREATE TABLE logintab (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
);

-- Seed logintab
INSERT INTO logintab (Username, Password) VALUES 
('admin', '12345'),
('mycodingproject', 'myc546'),
('my', 'myc');
GO

-- 2. Student CRUD Table (Raw ADO.NET)
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20)
);

-- 3. Librarian CRUD Table (Raw ADO.NET)
CREATE TABLE Librarians (
    LibrarianID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Phone NVARCHAR(20)
);

-- 4. Books CRUD Table (EF Core)
CREATE TABLE Books13 (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    ISBN NVARCHAR(50),
    PublishedDate DATETIME2 NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    ImageUrl NVARCHAR(500) NULL
);

-- 5. Borrow/Return Table (EF Core)
CREATE TABLE BorrowRecords13 (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    BookID INT NOT NULL,
    BorrowerName NVARCHAR(100) NOT NULL,
    BorrowerEmail NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    BorrowDate DATETIME2 NOT NULL,
    ReturnDate DATETIME2 NULL,
    FOREIGN KEY (BookID) REFERENCES Books13(ID) ON DELETE CASCADE
);

-- 6. Publications (Newspaper & Magazine) Table (EF Core)
CREATE TABLE Publications (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Publisher NVARCHAR(100) NOT NULL,
    PublishDate DATETIME2 NOT NULL,
    Type INT NOT NULL, -- 0 for Newspaper, 1 for Magazine
    IsAvailable BIT NOT NULL DEFAULT 1
);

-- Optional: Seed some dummy data for other tables
INSERT INTO Students (Name, Email, Phone) VALUES 
('Alice Smith', 'alice@example.com', '123-456-7890'),
('Bob Johnson', 'bob@example.com', '555-123-4567'),
('Charlie Brown', 'charlie@example.com', '999-888-7777'),
('David Wilson', 'david@example.com', '111-222-3333'),
('Emma Davis', 'emma@example.com', '444-555-6666');

INSERT INTO Librarians (Name, Age, Phone) VALUES 
('Jane Doe', 35, '111-222-3333'),
('John Smith', 42, '444-555-6666'),
('Michael Scott', 45, '123-456-7890'),
('Pam Beesly', 30, '987-654-3210');

INSERT INTO Books13 (Title, Author, ISBN, PublishedDate, IsAvailable, ImageUrl) VALUES 
('The Great Gatsby', 'F. Scott Fitzgerald', '978-0743273565', '1925-04-10', 1, '/images/gatsby_cover.png'),
('1984', 'George Orwell', '978-0451524935', '1949-06-08', 1, '/images/1984_cover.png'),
('To Kill a Mockingbird', 'Harper Lee', '978-0060935467', '1960-07-11', 1, '/images/mockingbird_cover.png'),
('Pride and Prejudice', 'Jane Austen', '978-0141439518', '1813-01-28', 1, '/images/pride_cover.png'),
('The Catcher in the Rye', 'J.D. Salinger', '978-0316769174', '1951-07-16', 1, '/images/catcher_cover.png');

INSERT INTO Publications (Title, Publisher, PublishDate, Type, IsAvailable) VALUES 
('The Daily News', 'News Corp', '2023-01-01', 0, 1),
('Tech Magazine', 'Tech Media', '2023-02-01', 1, 1),
('Global Times Newspaper', 'Global Media', '2023-05-15', 0, 1),
('Science Today Magazine', 'Science Weekly', '2023-06-20', 1, 1);
GO
