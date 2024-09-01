using CrmApiV2.Dtos.DynamicForm;
using CrmApiV2.Models.DynamicForm;

namespace CrmApiV2.Mapper.DynamicForm
{
    public static class RoleFormTemplateMapper
    {
        public static RoleFormTemplateDto ToRoleFormTemplateDto(this RoleFormTemplate roleFormTemplate)
        {
            return new RoleFormTemplateDto
            {
                RoleId = roleFormTemplate.RoleId,
                FormTemplateId = roleFormTemplate.FormTemplateId,
                CompanyId = roleFormTemplate.CompanyId
            };
        }

        public static RoleFormTemplate ToRoleFormTemplate(this RoleFormTemplateDto roleFormTemplateDto)
        {
            return new RoleFormTemplate
            {
                RoleId = roleFormTemplateDto.RoleId,
                FormTemplateId = roleFormTemplateDto.FormTemplateId,
                CompanyId = roleFormTemplateDto.CompanyId
            };
        }
    }
}
