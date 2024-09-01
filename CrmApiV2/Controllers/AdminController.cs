using CrmApiV2.Data;
using CrmApiV2.Dtos.DynamicForm;
using CrmApiV2.Dtos.Response;
using CrmApiV2.Interface;
using CrmApiV2.Mapper.DynamicForm;
using CrmApiV2.Models;
using CrmApiV2.Models.DynamicForm;
using CrmApiV2.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly CurrentUser user;

    public AdminController(ApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
        user = _currentUserService.GetCurrentUser();
    }

    [Authorize]
    [HttpPost("forms")]
    public async Task<IActionResult> CreateFormTemplate([FromBody] CreateFormTemplateDto createFormTemplateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<FormTemplateDto>
            {
                Status = SD.Failure,
                Message = "Invalid data"
            });
        }

        try
        {
            var formTemplate = createFormTemplateDto.ToFormTemplate(user.CompanyId);
            formTemplate.CompanyId = user.CompanyId;

            _context.FormTemplates.Add(formTemplate);
            await _context.SaveChangesAsync();

            var formTemplateDto = formTemplate.ToFormTemplateDto();

            var response = new ApiResponseDto<FormTemplateDto>
            {
                Status = SD.Success,
                Message = "Form template created successfully",
                Data = formTemplateDto
            };
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<FormTemplateDto>
            {
                Status = SD.Failure,
                Message = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost("forms/{formTemplateId}/fields")]
    public async Task<IActionResult> AddFieldToFormTemplate(int formTemplateId, [FromBody] CreateFormFieldDto createFormFieldDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<FormFieldDto>
            {
                Status = SD.Failure,
                Message = "Invalid data"
            });
        }

        try
        {
            var formTemplate = await _context.FormTemplates.FindAsync(formTemplateId);
            if (formTemplate == null || formTemplate.CompanyId != user.CompanyId)
            {
                return NotFound(new ApiResponseDto<FormFieldDto>
                {
                    Status = SD.Failure,
                    Message = "Form template not found or access denied"
                });
            }

            var formField = createFormFieldDto.ToFormField(user.CompanyId);
            formField.FormTemplateId = formTemplateId;
            formField.CompanyId = formTemplate.CompanyId;

            _context.FormFields.Add(formField);
            await _context.SaveChangesAsync();

            var formFieldDto = formField.ToFormFieldDto();

            var response = new ApiResponseDto<FormFieldDto>
            {
                Status = SD.Success,
                Message = "Field added successfully",
                Data = formFieldDto
            };
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<FormFieldDto>
            {
                Status = SD.Failure,
                Message = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost("forms/{formTemplateId}/assign-roles")]
    public async Task<IActionResult> AssignFormToRole(int formTemplateId, [FromBody] RoleDto roleInput)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<RoleFormTemplateDto>
            {
                Status = SD.Failure,
                Message = "Invalid data"
            });
        }

        try
        {
            var formTemplate = await _context.FormTemplates.FindAsync(formTemplateId);
            var role = await _context.Roles.Where(x => x.Name == roleInput.Role).FirstOrDefaultAsync();

            if (role == null || formTemplate == null || formTemplate.CompanyId != user.CompanyId)
            {
                return NotFound(new ApiResponseDto<RoleFormTemplateDto>
                {
                    Status = SD.Failure,
                    Message = "Role or form template not found or access denied"
                });
            }

            var roleFormTemplate = new RoleFormTemplate
            {
                RoleId = role.Id,
                FormTemplateId = formTemplateId,
                CompanyId = formTemplate.CompanyId
            };

            _context.RoleFormTemplates.Add(roleFormTemplate);
            await _context.SaveChangesAsync();

            var roleFormTemplateDto = roleFormTemplate.ToRoleFormTemplateDto();

            var response = new ApiResponseDto<RoleFormTemplateDto>
            {
                Status = SD.Success,
                Message = "Form template assigned to role successfully",
                Data = roleFormTemplateDto
            };
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponseDto<RoleFormTemplateDto>
            {
                Status = SD.Failure,
                Message = ex.Message
            });
        }
    }
}