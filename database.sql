CREATE DATABASE AttendanceSystem;
USE AttendanceSystem;

-- Disable FK checks to allow referencing before definition
SET FOREIGN_KEY_CHECKS = 0;

-- Users with login credentials
CREATE TABLE Professor (
    ProfessorID INT AUTO_INCREMENT PRIMARY KEY,
    ID VARCHAR(10) NOT NULL UNIQUE,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Username VARCHAR(25) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

-- Imported from CSV, no login credentials
CREATE TABLE Student (
    StudentID INT AUTO_INCREMENT PRIMARY KEY,
    UTDID VARCHAR(10) NOT NULL UNIQUE,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Username VARCHAR(10) UNIQUE NOT NULL
);

-- Courses Table
CREATE TABLE Course (
    CourseID INT AUTO_INCREMENT PRIMARY KEY,
    CourseNumber VARCHAR(10) NOT NULL,
    CourseName VARCHAR(255) NOT NULL,
    Section VARCHAR(10) NOT NULL,
    ProfessorID VARCHAR(10) NOT NULL,
    FOREIGN KEY (ProfessorID) REFERENCES Professor(ID) ON DELETE CASCADE
);

CREATE TABLE CourseEnrollment (
    EnrollmentID INT AUTO_INCREMENT PRIMARY KEY,
    EnrollmentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    CourseID INT NOT NULL,
    UTDID VARCHAR(10) NOT NULL,
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE,
    FOREIGN KEY (UTDID) REFERENCES Student(UTDID) ON DELETE CASCADE,
    UNIQUE KEY (CourseID, UTDID) -- Prevent duplicate enrollments
);

-- Each class meeting with password/quiz config
CREATE TABLE ClassSession (
    SessionID INT AUTO_INCREMENT PRIMARY KEY,
    SessionDateTime DATETIME NOT NULL,
    QuizStartTime DATETIME NOT NULL,
    QuizEndTime DATETIME NOT NULL,
    CourseID INT NOT NULL,
    AccessCode VARCHAR(36) NOT NULL DEFAULT (UUID()) UNIQUE, -- this value is automatically generated and will be used as the access code for the session (EX: http://localhost:5225/123e4567-e89b-12d3-a456-426614174000)
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE
);

-- Map questions to sessions (which questions used in each session)
CREATE TABLE SessionQuestion (
    SessionQuestionID INT AUTO_INCREMENT PRIMARY KEY,
    SessionID INT NOT NULL,
    QuestionID INT NOT NULL,
    FOREIGN KEY (SessionID) REFERENCES ClassSession(SessionID) ON DELETE CASCADE,
    FOREIGN KEY (QuestionID) REFERENCES QuizQuestion(QuestionID) ON DELETE CASCADE,
    UNIQUE KEY (SessionID, QuestionID)
);

-- Group of questions for a session
CREATE TABLE QuizQuestionBank (
    QuestionBankID INT AUTO_INCREMENT PRIMARY KEY,
    BankName VARCHAR(255) NOT NULL,
    CourseID INT NOT NULL,
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE
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
    Answer INT NOT NULL,
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

-- Sample Data Insertion

-- Re-enable FK checks
SET FOREIGN_KEY_CHECKS = 1;