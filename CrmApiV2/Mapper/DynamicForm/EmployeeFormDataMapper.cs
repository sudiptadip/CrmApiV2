using CrmApiV2.Dtos.DynamicForm;
using CrmApiV2.Models.DynamicForm;

namespace CrmApiV2.Mapper.DynamicForm
{
    public static class EmployeeFormDataMapper
    {
        public static EmployeeFormDataDto ToEmployeeFormDataDto(this EmployeeFormData employeeFormData)
        {
            return new EmployeeFormDataDto
            {
                FieldId = employeeFormData.FieldId,
                FieldName = employeeFormData.FormField.FieldName,
                FieldValue = employeeFormData.FieldValue
            };
        }

        public static EmployeeFormData ToEmployeeFormData(this EmployeeFormDataDto employeeFormDataDto, string userId, int companyId)
        {
            return new EmployeeFormData
            {
                UserId = userId,
                FieldId = employeeFormDataDto.FieldId,
                FieldValue = employeeFormDataDto.FieldValue,
                CompanyId = companyId
            };
        }

    }
}
