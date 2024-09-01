using CrmApiV2.Models.DynamicForm;

namespace CrmApiV2.Dtos.DynamicForm
{
    public class EmployeeFormDataDto
    {
        public int FieldId { get; set; }  // For retrieval or update
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        // Navigation property
        public FormField FormField { get; set; }
    }
}
