using CrmApiV2.Models.DynamicForm;
using Microsoft.AspNetCore.Identity;

namespace CrmApiV2.Models.Register
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<RoleFormTemplate> RoleFormTemplates { get; set; }
    }
}
