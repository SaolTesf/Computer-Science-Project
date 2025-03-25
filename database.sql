CREATE DATABASE AttendanceSystem;
USE AttendanceSystem;

-- Users with login credentials
CREATE TABLE Professor (
    ID VARCHAR(10) PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Username VARCHAR(25) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

-- Imported from CSV, no login credentials
CREATE TABLE Student (
    UTDID VARCHAR(10) PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL
);

-- Courses Table
CREATE TABLE Course (
    CourseNumber VARCHAR(10) PRIMARY KEY,
    CourseName VARCHAR(255) NOT NULL,
    Section VARCHAR(10) NOT NULL,
    ProfessorID VARCHAR(10) NOT NULL,
    FOREIGN KEY (ProfessorID) REFERENCES Professor(ID) ON DELETE CASCADE
);

-- Each class meeting with password/quiz config
CREATE TABLE ClassSession (
    SessionID INT AUTO_INCREMENT PRIMARY KEY,
    CourseNumber VARCHAR(10) NOT NULL,
    SessionDateTime DATETIME NOT NULL,
    Password VARCHAR(255) NOT NULL,
    QuizStartTime DATETIME NOT NULL,
    QuizEndTime DATETIME NOT NULL,
    QuestionBankID INT NOT NULL,
    FOREIGN KEY (CourseNumber) REFERENCES Course(CourseNumber) ON DELETE CASCADE
);

-- Group of questions for a session
CREATE TABLE QuizQuestionBank (
    QuestionBankID INT AUTO_INCREMENT PRIMARY KEY,
    BankName VARCHAR(255) NOT NULL,
    CourseNumber VARCHAR(10) NOT NULL,
    FOREIGN KEY (CourseNumber) REFERENCES Course(CourseNumber) ON DELETE CASCADE
);

-- Quiz Questions - Multiple-choice
CREATE TABLE QuizQuestion (
    QuestionID INT AUTO_INCREMENT PRIMARY KEY,
    QuestionBankID INT NOT NULL,
    QuestionText TEXT NOT NULL,
    Option1 TEXT NOT NULL,
    Option2 TEXT NOT NULL,
    Option3 TEXT,
    Option4 TEXT,
    FOREIGN KEY (QuestionBankID) REFERENCES QuizQuestionBank(QuestionBankID) ON DELETE CASCADE
);

-- Student submissions
CREATE TABLE Attendance (
    AttendanceID INT AUTO_INCREMENT PRIMARY KEY,
    SessionID INT NOT NULL,
    UTDID VARCHAR(10) NOT NULL,
    SubmissionTime DATETIME NOT NULL,
    IPAddress VARCHAR(45) NOT NULL,
    AttendanceType ENUM('Present', 'Excused', 'Unexcused') NOT NULL DEFAULT 'Present',
    FOREIGN KEY (SessionID) REFERENCES ClassSession(SessionID) ON DELETE CASCADE,
    FOREIGN KEY (UTDID) REFERENCES Student(UTDID) ON DELETE CASCADE
);

-- Quiz Responses (Student answers)
CREATE TABLE QuizResponse (
    ResponseID INT AUTO_INCREMENT PRIMARY KEY,
    AttendanceID INT NOT NULL,
    QuestionID INT NOT NULL,
    SelectedOption INT NOT NULL,
    FOREIGN KEY (AttendanceID) REFERENCES Attendance(AttendanceID) ON DELETE CASCADE,
    FOREIGN KEY (QuestionID) REFERENCES QuizQuestion(QuestionID) ON DELETE CASCADE
);