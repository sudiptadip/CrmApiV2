using Microsoft.AspNetCore.Http;

namespace CrmApiV2.Models.DynamicForm
{
    public class FormTemplate
    {
        public int FormTemplateId { get; set; }
        public string FormName { get; set; }
        public int CompanyId { get; set; }

        public ICollection<FormField> FormFields { get; set; }
        public ICollection<RoleFormTemplate> RoleFormTemplates { get; set; }
    }
}