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
        if(response.IsSuccessStatusCode)
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
    }
    // Additional methods to be added later
}