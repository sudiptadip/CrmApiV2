namespace CrmApiV2.Dtos.DynamicForm
{
    public class CreateFormFieldDto
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OtherValue { get; set; }
        public bool IsRequired { get; set; }
    }
}
