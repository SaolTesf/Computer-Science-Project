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
    LastName VARCHAR(255) NOT NULL,
    Username VARCHAR(10) UNIQUE NOT NULL
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

-- Sample Data Insertion

-- Insert Professors (no dependencies)
INSERT INTO Professor (ID, FirstName, LastName, Username, Email, PasswordHash)
VALUES
   ('5400000001', 'John', 'Smith', 'jsmith', 'john.smith@example.com', 'hashedpassword1'),
   ('5400000002', 'Jane', 'Doe', 'jdoe', 'jane.doe@example.com', 'hashedpassword2'),
   ('5400000003', 'Robert', 'Johnson', 'rjohnson', 'robert.johnson@example.com', 'hashedpassword3');

-- Insert Students (no dependencies)
INSERT INTO Student (UTDID, FirstName, LastName, Username)
VALUES
     ('2100000001', 'Alice', 'Williams', 'axw3456432'),
     ('2100000002', 'Bob', 'Brown', 'bxb3456432'),
     ('2100000003', 'Charlie', 'Davis', 'cxd3456432'),
     ('2100000004', 'Diana', 'Evans', 'dxe3456432'),
     ('2100000005', 'Ethan', 'Fisher', 'exf3456432');

-- Insert Courses (depends on Professor)
INSERT INTO Course (CourseNumber, CourseName, Section, ProfessorID)
VALUES
    ('CS4485', 'Computer Science Project', '001', '5400000001'),
    ('CS4361', 'Computer Graphics', '002', '5400000002'),
    ('CS4390', 'Computer Networks', '001', '5400000003'),
    ('CS4348', 'Operating Systems', '003', '5400000001');

-- Insert QuizQuestionBanks (depends on Course)
INSERT INTO QuizQuestionBank (QuestionBankID, BankName, CourseNumber)
VALUES
    (1, 'Week 1 Quiz', 'CS4485'),
    (2, 'Week 2 Quiz', 'CS4485'),
    (3, 'Midterm Review', 'CS4361'),
    (4, 'Final Review', 'CS4390');

-- Insert QuizQuestions (depends on QuizQuestionBank)
INSERT INTO QuizQuestion (QuestionID, QuestionBankID, QuestionText, Option1, Option2, Option3, Option4)
VALUES
    (1, 1, 'Sample question 1 for Week 1?', 'Option A', 'Option B', 'Option C', 'Option D'),
    (2, 1, 'Sample question 2 for Week 1?', 'Option A', 'Option B', 'Option C', 'Option D'),
    (3, 2, 'Sample question 1 for Week 2?', 'Option A', 'Option B', 'Option C', 'Option D'),
    (4, 3, 'Sample question 1 for Midterm Review?', 'Option A', 'Option B', 'Option C', 'Option D'),
    (5, 4, 'Sample question 1 for Final Review?', 'Option A', 'Option B', 'Option C', 'Option D');

-- Insert ClassSessions (depends on Course and QuizQuestionBank)
INSERT INTO ClassSession (SessionID, CourseNumber, SessionDateTime, Password, QuizStartTime, QuizEndTime, QuestionBankID)
VALUES
    (1, 'CS4485', '2025-03-24 10:00:00', 'password123', '2025-03-24 10:30:00', '2025-03-24 10:45:00', 1),
    (2, 'CS4485', '2025-03-31 10:00:00', 'password456', '2025-03-31 10:30:00', '2025-03-31 10:45:00', 2),
    (3, 'CS4361', '2025-04-02 14:00:00', 'password789', '2025-04-02 14:30:00', '2025-04-02 14:45:00', 3),
    (4, 'CS4390', '2025-04-04 15:00:00', 'passwordabc', '2025-04-04 15:30:00', '2025-04-04 15:45:00', 4);

-- Insert Attendance (depends on ClassSession and Student)
INSERT INTO Attendance (AttendanceID, SessionID, UTDID, SubmissionTime, IPAddress, AttendanceType)
VALUES
    (1, 1, '2100000001', '2025-03-24 10:05:00', '192.168.1.101', 'Present'),
    (2, 1, '2100000002', '2025-03-24 10:06:00', '192.168.1.102', 'Present'),
    (3, 1, '2100000003', '2025-03-24 10:07:00', '192.168.1.103', 'Present'),
    (4, 2, '2100000001', '2025-03-31 10:04:00', '192.168.1.101', 'Present'),
    (5, 2, '2100000002', '2025-03-31 10:05:00', '192.168.1.102', 'Present'),
    (6, 3, '2100000004', '2025-04-02 14:03:00', '192.168.1.104', 'Present'),
    (7, 3, '2100000005', '2025-04-02 14:04:00', '192.168.1.105', 'Present'),
    (8, 4, '2100000003', '2025-04-04 15:10:00', '192.168.1.103', 'Present'),
    (9, 1, '2100000004', '2025-03-24 10:00:00', '192.168.1.104', 'Unexcused'),
    (10, 2, '2100000003', '2025-03-31 10:30:00', '192.168.1.103', 'Excused');

-- Insert QuizResponses (depends on Attendance and QuizQuestion)
INSERT INTO QuizResponse (ResponseID, AttendanceID, QuestionID, SelectedOption)
VALUES
    (1, 1, 1, 2),  -- Alice chose option 2 for question 1
    (2, 1, 2, 3),  -- Alice chose option 3 for question 2
    (3, 2, 1, 2),  -- Bob chose option 2 for question 1
    (4, 2, 2, 3),  -- Bob chose option 3 for question 2
    (5, 3, 1, 1),  -- Charlie chose option 1 for question 1
    (6, 3, 2, 3),  -- Charlie chose option 3 for question 2
    (7, 4, 3, 1),  -- Alice chose option 1 for question 3
    (8, 5, 3, 1),  -- Bob chose option 1 for question 3
    (9, 6, 4, 3),  -- Diana chose option 3 for question 4
    (10, 7, 4, 3); -- Ethan chose option 3 for question 4