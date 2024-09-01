namespace CrmApiV2.Dtos.DynamicForm
{
    public class CreateFormTemplateDto
    {
        public string FormName { get; set; }
        public List<CreateFormFieldDto> FormFields { get; set; }
    }
}
