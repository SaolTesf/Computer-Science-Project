-- Disable foreign‑key checks so we can drop tables in any order
SET FOREIGN_KEY_CHECKS = 0;

-- Switch into the AttendanceSystem database (if not already)
USE AttendanceSystem;

-- Drop tables, from most dependent down to base
DROP TABLE IF EXISTS `QuizResponse`;
DROP TABLE IF EXISTS `Attendance`;
DROP TABLE IF EXISTS `QuizQuestion`;
DROP TABLE IF EXISTS `QuizQuestionBank`;
DROP TABLE IF EXISTS `SessionQuestion`;
DROP TABLE IF EXISTS `ClassSession`;
DROP TABLE IF EXISTS `CourseEnrollment`;
DROP TABLE IF EXISTS `Course`;
DROP TABLE IF EXISTS `Student`;
DROP TABLE IF EXISTS `Professor`;

-- Re‑enable foreign‑key checks
SET FOREIGN_KEY_CHECKS = 1;

-- Finally, drop the database
DROP DATABASE IF EXISTS `AttendanceSystem`;