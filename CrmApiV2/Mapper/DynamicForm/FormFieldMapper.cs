using CrmApiV2.Dtos.DynamicForm;
using CrmApiV2.Models.DynamicForm;

namespace CrmApiV2.Mapper.DynamicForm
{
    public static class FormFieldMapper
    {
        public static FormFieldDto ToFormFieldDto(this FormField formField)
        {
            return new FormFieldDto
            {
                FieldId = formField.FieldId,
                FieldName = formField.FieldName,
                FieldType = formField.FieldType,
                IsRequired = formField.IsRequired,
                OtherValue = formField.OtherValue,
                CompanyId = formField.CompanyId
            };
        }

        public static FormField ToFormField(this CreateFormFieldDto createFormFieldDto, int companyId)
        {
            return new FormField
            {
                FieldName = createFormFieldDto.FieldName,
                FieldType = createFormFieldDto.FieldType,
                IsRequired = createFormFieldDto.IsRequired,
                OtherValue = createFormFieldDto.OtherValue,
                CompanyId = companyId
            };
        }
    }
}
