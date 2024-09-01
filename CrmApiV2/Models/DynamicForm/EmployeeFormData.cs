using CrmApiV2.Models.Register;

namespace CrmApiV2.Models.DynamicForm
{
    public class EmployeeFormData
    {
        public int EmployeeFormDataId { get; set; }
        public string UserId { get; set; }
        public int FieldId { get; set; }
        public string FieldValue { get; set; }
        public int CompanyId { get; set; }

        public ApplicationUser User { get; set; }
        public FormField FormField { get; set; }
    }
}