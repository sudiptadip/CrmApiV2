namespace CrmApiV2.Dtos.DynamicForm
{
    public class FormFieldDto
    {
        public int FieldId { get; set; }  // For retrieval or update
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OtherValue { get; set; }
        public bool IsRequired { get; set; }
        public int CompanyId { get; set; }
    }
}
