using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Models.DynamicForm
{
    public class FormField
    {
        [Key]
        public int FieldId { get; set; }
        public int FormTemplateId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OtherValue { get; set; }
        public bool IsRequired { get; set; }
        public int CompanyId { get; set; }

        public FormTemplate FormTemplate { get; set; }
    }
}
