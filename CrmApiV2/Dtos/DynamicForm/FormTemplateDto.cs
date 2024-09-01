namespace CrmApiV2.Dtos.DynamicForm
{
    public class FormTemplateDto
    {
        public int FormTemplateId { get; set; }  // For retrieval or update
        public string FormName { get; set; }
        public int CompanyId { get; set; }
        public List<FormFieldDto> FormFields { get; set; }
    }
}
