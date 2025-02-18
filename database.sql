CREATE DATABASE AttendanceSystem;
USE AttendanceSystem;

-- Users Table
CREATE TABLE Users (
    ID VARCHAR(10) PRIMARY KEY, -- UTD ID, Unique, 10 digits
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    Username VARCHAR(9) UNIQUE NOT NULL, -- Net ID
    Email VARCHAR(255) UNIQUE,
    PasswordHash VARCHAR(255), -- Store hashed passwords
    UserType ENUM('Student', 'Professor') NOT NULL -- Differentiates roles
);

-- Students Table (inherits Users)
CREATE TABLE Students (
    StudentID VARCHAR(10) PRIMARY KEY, -- Matches Users.ID
    FOREIGN KEY (StudentID) REFERENCES Users(ID) ON DELETE CASCADE
);

-- Professors Table (inherits Users)
CREATE TABLE Professors (
    ProfessorID VARCHAR(10) PRIMARY KEY, -- Matches Users.ID
    FOREIGN KEY (ProfessorID) REFERENCES Users(ID) ON DELETE CASCADE
);

-- Courses Table
CREATE TABLE Courses (
    CourseNumber VARCHAR(10) PRIMARY KEY,
    CourseName TEXT NOT NULL,
    Section VARCHAR(10) NOT NULL,
    ProfessorID VARCHAR(10),
    FOREIGN KEY (ProfessorID) REFERENCES Professors(ProfessorID) ON DELETE SET NULL
);

-- Attendance Table
CREATE TABLE Attendance (
    AttendanceID INT AUTO_INCREMENT PRIMARY KEY,
    UserID VARCHAR(10), -- Foreign key from Users
    CourseNumber VARCHAR(10), -- Foreign key from Courses
    AttendanceDate DATE NOT NULL,
    AttendanceType ENUM('Present', 'Excused', 'Unexcused') NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE,
    FOREIGN KEY (CourseNumber) REFERENCES Courses(CourseNumber) ON DELETE CASCADE
);

-- DaysMissed Table
CREATE TABLE DaysMissed (
    UserID VARCHAR(10), -- Foreign key from Users
    MissedDate DATE NOT NULL,
    PRIMARY KEY (UserID, MissedDate),
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE
);
