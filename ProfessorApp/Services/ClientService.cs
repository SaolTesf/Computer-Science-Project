using System.Net.Http.Json;
using AttendanceShared.DTOs;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ProfessorApp.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;
        public string? ProfessorId { get; private set; }
        public ProfessorDTO? CurrentProfessor { get; private set; }

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDTO?> LoginAsync(string identifier, string password)
        {
            var loginDto = new LoginDTO { Identifier = identifier, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var auth = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
                if (auth != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token);
                    ProfessorId = auth.User.ID;
                    CurrentProfessor = auth.User;
                }
                return auth;
            }
            return null;
        }

        // gets register address and waits for a response
        public async Task<AuthResponseDTO?> RegisterAsync(string id, string firstName, string lastName, string username, string email, string password)
        {
            var registerDto = new RegisterDTO { ID = id, FirstName = firstName, LastName = lastName, Username = username, Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
            }
            return null;
        }


        // Student Methods
        public async Task<List<StudentDTO>?> GetAllStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StudentDTO>>("api/student");
        }

        public async Task<StudentDTO?> GetStudentByUTDIdAsync(string utdId)
        {
            return await _httpClient.GetFromJsonAsync<StudentDTO>($"api/student/id/{utdId}");
        }

        public async Task<StudentDTO?> GetStudentByUsernameAsync(string username)
        {
            return await _httpClient.GetFromJsonAsync<StudentDTO>($"api/student/username/{username}");
        }

        public async Task<bool> AddStudentAsync(StudentDTO student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/student", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStudentAsync(StudentDTO student)
        {
            var response = await _httpClient.PutAsJsonAsync("api/student", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<string?> DeleteStudentByUTDIdAsync(string utdId)
        {
            var response = await _httpClient.DeleteAsync($"api/student/{utdId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); 
            }
            return null;
        }

        // QuizBank Methods
        public async Task<List<QuizQuestionBankDTO>?> GetAllQuizQuestionBanksAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<QuizQuestionBankDTO>>("api/quizquestionbank");
            return response;
        }

        public async Task<List<QuizQuestionBankDTO>?> GetQuizQuestionBankByIDAsync(int bankId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<QuizQuestionBankDTO>>($"api/quizquestionbank/{bankId}");
            return response;
        }

        public async Task<bool> CreateQuizQuestionBankAsync(QuizQuestionBankDTO bank)
        {
            var response = await _httpClient.PostAsJsonAsync("api/quizquestionbank", bank);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuizQuestionBankAsync(QuizQuestionBankDTO bank)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/quizquestionbank/{bank.QuestionBankID}", bank);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteQuizQuestionBankAsync(int bankId)
        {
            var response = await _httpClient.DeleteAsync($"api/quizquestionbank/{bankId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<string>?> GetAllQuizBankNamesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>("api/quizquestionbank/banknames");
            return response;
        }


        // Quizquestion Methods
        public async Task<List<QuizQuestionDTO>?> GetAllQuizQuestionAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<QuizQuestionDTO>>("api/quizquestion");
            return response;
        }

       public async Task<List<QuizQuestionDTO>?> GetQuizQuestionByIdAsync(int questionId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<QuizQuestionDTO>>($"api/quizquestion/{questionId}");
            return response;
        }
        public async Task<bool> CreateQuizQuestionAsync(QuizQuestionDTO question)
        {
            var response = await _httpClient.PostAsJsonAsync("api/quizquestion", question);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuizQuestionAsync(QuizQuestionDTO question)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/quizquestion/{question.QuestionID}", question);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteQuizQuestionAsync(int questionId)
        {
            var response = await _httpClient.DeleteAsync($"api/quizquestion/{questionId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<int?> GetQuizBankIdByNameAsync(string bankName)
        {
            var response = await _httpClient.GetAsync($"api/quizquestionbank/GetBankIdByName?bankName={Uri.EscapeDataString(bankName)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            return null;
        }

        // Courses
        public async Task<List<CourseDTO>?> GetAllCoursesAsync()
            => await _httpClient.GetFromJsonAsync<List<CourseDTO>>("api/course");

        public async Task<List<CourseDTO>?> GetCoursesByProfessorAsync()
        {
            if (ProfessorId == null) return null;
            return await _httpClient.GetFromJsonAsync<List<CourseDTO>>($"api/course/professor/{ProfessorId}");
        }

        public async Task<bool> AddCourseAsync(CourseDTO course)
        {
            var response = await _httpClient.PostAsJsonAsync("api/course", course);
            return response.IsSuccessStatusCode;
        }
        public async Task<CourseDTO?> GetCourseByIdAsync(int? id)
        {
            return await _httpClient.GetFromJsonAsync<CourseDTO>($"api/course/{id}");
        }
        public async Task<string?> DeleteCourseByIDAsync(int? id)
        {
            var response = await _httpClient.DeleteAsync($"api/course/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // returns "<Name> has been removed."
            }
            return null;
        }

        // Enrollments
        public async Task<List<CourseEnrollmentDetailDTO>?> GetEnrollmentsAsync(int? courseID)
            => await _httpClient.GetFromJsonAsync<List<CourseEnrollmentDetailDTO>>($"api/courseenrollment/course/{courseID}");

        public async Task<bool> EnrollStudentToCourseAsync(CourseEnrollmentDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/courseenrollment", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnenrollStudentAsync(int enrollmentId)
        {
            var response = await _httpClient.DeleteAsync($"api/courseenrollment/{enrollmentId}");
            return response.IsSuccessStatusCode;
        }

    }
}