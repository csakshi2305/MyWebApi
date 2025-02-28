using MyWebApi.Interfaces;
using System.Security.Claims;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class APIUserContext : IAPIUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public APIUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                return int.TryParse(userIdClaim?.Value, out var userId) ? userId : 0;
            }
        }
    }

}
