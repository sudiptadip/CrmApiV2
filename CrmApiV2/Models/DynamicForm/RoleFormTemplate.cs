using CrmApiV2.Models.Register;

namespace CrmApiV2.Models.DynamicForm
{
    public class RoleFormTemplate
    {
        public int RoleFormTemplateId { get; set; }
        public string RoleId { get; set; }  // IdentityRole Id
        public int FormTemplateId { get; set; }
        public int CompanyId { get; set; }

        public ApplicationRole Role { get; set; }
        public FormTemplate FormTemplate { get; set; }
    }
}
