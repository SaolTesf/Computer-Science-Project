/*Diego: Service to get Client IP Address*/
namespace AttendanceSystem.Services
{
    public class GetIPService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetIPService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClientIp()
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            return remoteIpAddress ?? "IP not available";
        }
    }

}
