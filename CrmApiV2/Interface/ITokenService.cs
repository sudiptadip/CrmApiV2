using CrmApiV2.Models.Register;

namespace CrmApiV2.Interface
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
