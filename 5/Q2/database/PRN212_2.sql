IF DB_ID(N'PRN212_2') IS NOT NULL
BEGIN
    ALTER DATABASE [PRN212_2] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [PRN212_2];
END
GO
CREATE DATABASE [PRN212_2];
GO
USE [PRN212_2];
GO
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);
GO
CREATE TABLE Instructors (
    InstructorId INT IDENTITY(1,1) PRIMARY KEY,
    InstructorName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL
);
GO
CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseTitle NVARCHAR(150) NOT NULL,
    CourseCode NVARCHAR(50) NOT NULL,
    DurationHours INT NOT NULL,
    TuitionFee DECIMAL(18,2) NOT NULL,
    CategoryId INT NOT NULL,
    InstructorId INT NOT NULL,
    CONSTRAINT FK_Courses_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CONSTRAINT FK_Courses_Instructors FOREIGN KEY (InstructorId) REFERENCES Instructors(InstructorId)
);
GO
INSERT INTO Categories (CategoryName) VALUES
(N'Programming'),
(N'Data'),
(N'Design'),
(N'Business'),
(N'Testing');
GO
INSERT INTO Instructors (InstructorName, Email) VALUES
(N'Nguyen Van A', N'a@center.vn'),
(N'Tran Thi B', N'b@center.vn'),
(N'Le Van C', N'c@center.vn'),
(N'Pham Thi D', N'd@center.vn'),
(N'Hoang Van E', N'e@center.vn');
GO
INSERT INTO Courses (CourseTitle, CourseCode, DurationHours, TuitionFee, CategoryId, InstructorId) VALUES
(N'C# Basic', N'CS101', 30, 2500000, 1, 1),
(N'WPF Basic', N'WP101', 24, 2200000, 1, 2),
(N'SQL Foundation', N'DB101', 20, 2000000, 2, 3),
(N'UI Design', N'UI101', 18, 1800000, 3, 4),
(N'BA Essentials', N'BA101', 16, 1700000, 4, 5),
(N'Testing Basic', N'TE101', 15, 1500000, 5, 1),
(N'ASP.NET Core', N'AS101', 32, 3000000, 1, 2),
(N'Data Analysis', N'DA101', 28, 2800000, 2, 3);
GO
SELECT * FROM Categories;
SELECT * FROM Instructors;
SELECT * FROM Courses;
GO