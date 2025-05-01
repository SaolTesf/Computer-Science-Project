using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedQuizQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SelectedQuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    QuestionText = table.Column<string>(type: "longtext", nullable: false),
                    QuestionBankID = table.Column<int>(type: "int", nullable: false),
                    SelectedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedQuizQuestions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    UTDID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.UTDID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CourseNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CourseName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Section = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ProfessorID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_Professor_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CourseEnrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    UTDID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrollment", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_CourseEnrollment_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEnrollment_Student_UTDID",
                        column: x => x.UTDID,
                        principalTable: "Student",
                        principalColumn: "UTDID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizQuestionBank",
                columns: table => new
                {
                    QuestionBankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BankName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionBank", x => x.QuestionBankID);
                    table.ForeignKey(
                        name: "FK_QuizQuestionBank_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassSession",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    SessionDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    QuizStartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    QuizEndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    QuestionBankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSession", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_ClassSession_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSession_QuizQuestionBank_QuestionBankID",
                        column: x => x.QuestionBankID,
                        principalTable: "QuizQuestionBank",
                        principalColumn: "QuestionBankID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    QuestionBankID = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "longtext", nullable: false),
                    Option1 = table.Column<string>(type: "longtext", nullable: false),
                    Option2 = table.Column<string>(type: "longtext", nullable: false),
                    Option3 = table.Column<string>(type: "longtext", nullable: true),
                    Option4 = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_QuizQuestionBank_QuestionBankID",
                        column: x => x.QuestionBankID,
                        principalTable: "QuizQuestionBank",
                        principalColumn: "QuestionBankID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SessionID = table.Column<int>(type: "int", nullable: false),
                    UTDID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IPAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    AttendanceType = table.Column<string>(type: "longtext", nullable: false, defaultValue: "Present")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendance_ClassSession_SessionID",
                        column: x => x.SessionID,
                        principalTable: "ClassSession",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizResponse",
                columns: table => new
                {
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AttendanceID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    SelectedOption = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResponse", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK_QuizResponse_Attendance_AttendanceID",
                        column: x => x.AttendanceID,
                        principalTable: "Attendance",
                        principalColumn: "AttendanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizResponse_QuizQuestion_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QuizQuestion",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_SessionID",
                table: "Attendance",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSession_CourseID",
                table: "ClassSession",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSession_QuestionBankID",
                table: "ClassSession",
                column: "QuestionBankID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProfessorID",
                table: "Course",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollment_CourseID",
                table: "CourseEnrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollment_UTDID",
                table: "CourseEnrollment",
                column: "UTDID");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Email",
                table: "Professor",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Username",
                table: "Professor",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuestionBankID",
                table: "QuizQuestion",
                column: "QuestionBankID");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionBank_CourseID",
                table: "QuizQuestionBank",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResponse_AttendanceID",
                table: "QuizResponse",
                column: "AttendanceID");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResponse_QuestionID",
                table: "QuizResponse",
                column: "QuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEnrollment");

            migrationBuilder.DropTable(
                name: "QuizResponse");

            migrationBuilder.DropTable(
                name: "SelectedQuizQuestions");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "ClassSession");

            migrationBuilder.DropTable(
                name: "QuizQuestionBank");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}
