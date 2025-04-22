using System.Net.Http.Json;
using AttendanceShared.DTOs;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProfessorApp.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;

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
                return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
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


        // Additional methods to be added later
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
                return await response.Content.ReadAsStringAsync(); // returns "<Name> has been removed."
            }
            return null;
        }
    }
}