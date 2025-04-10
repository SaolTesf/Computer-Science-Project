using System.Net.Http.Json;
using AttendanceShared.DTOs;
using System.Threading.Tasks;

namespace ProfessorApp.Services
{
  public class ClientService
  {
    private readonly HttpClient _httpClient;

    public ClientService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<AuthResponseDTO?> LoginAsync(string username, string password)
    {
        var loginDto = new LoginDTO { Username = username, Password = password };
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);
        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
        }
        return null;
    }

    // Additional methods to be added later like register
  }
}