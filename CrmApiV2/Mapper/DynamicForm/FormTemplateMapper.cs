using CrmApiV2.Dtos.DynamicForm;
using CrmApiV2.Models.DynamicForm;

namespace CrmApiV2.Mapper.DynamicForm
{
    public static class FormTemplateMapper
    {
        public static FormTemplateDto ToFormTemplateDto(this FormTemplate formTemplate)
        {
            return new FormTemplateDto
            {
                FormTemplateId = formTemplate.FormTemplateId,
                FormName = formTemplate.FormName,
                CompanyId = formTemplate.CompanyId,               
                FormFields = formTemplate.FormFields?.Select(f => f.ToFormFieldDto()).ToList()
            };
        }

        public static FormTemplate ToFormTemplate(this CreateFormTemplateDto createFormTemplateDto, int companyId)
        {
            return new FormTemplate
            {
                FormName = createFormTemplateDto.FormName,
                CompanyId = companyId,
                FormFields = createFormTemplateDto.FormFields?.Select(f => f.ToFormField(companyId)).ToList()
            };
        }
    }
}
