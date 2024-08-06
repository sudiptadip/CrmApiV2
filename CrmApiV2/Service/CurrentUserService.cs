using CrmApiV2.Data;
using CrmApiV2.Interface;
using CrmApiV2.Models;
using System.Security.Claims;

namespace CrmApiV2.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }

        public CurrentUser GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            string id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var currUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (currUser == null)
            {
                return null;
            }

            return new CurrentUser
            {
                Id = id,
                UserName = currUser.UserName,
                Email = currUser.Email,
                Roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList(),
                Name = currUser.Name,
                Address = currUser.Address,
                CompanyId = currUser.CompanyId,
            };
        }
    }
}
