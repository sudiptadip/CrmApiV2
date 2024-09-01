namespace CrmApiV2.Dtos.DynamicForm
{
    public class EmployeeFormDataResponseDto
    {
        public int FormTemplateId { get; set; }
        public string FormName { get; set; }
        public List<EmployeeFormDataDto> FormData { get; set; }
    }
}
