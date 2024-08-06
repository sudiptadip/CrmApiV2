using CrmApiV2.Models;

namespace CrmApiV2.Interface
{
    public interface ICurrentUserService
    {
        CurrentUser GetCurrentUser();
    }
}
