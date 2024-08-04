using CrmApiV2.Models;

namespace CrmApiV2.Interface
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
