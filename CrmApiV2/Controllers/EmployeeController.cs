using CrmApiV2.Data;
using CrmApiV2.Dtos.DynamicForm;
using CrmApiV2.Dtos.Response;
using CrmApiV2.Mapper.DynamicForm;
using CrmApiV2.Models.DynamicForm;
using CrmApiV2.Models.Register;
using CrmApiV2.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public EmployeeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("forms")]
    [Authorize]
    public async Task<IActionResult> GetFormsForEmployee()
    {
        var user = await _userManager.GetUserAsync(User);

        var roles = await _userManager.GetRolesAsync(user);
        var roleName = roles.FirstOrDefault();

        if (roleName == null)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Status = SD.Failure,
                Message = "Role not found."
            });
        }

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Status = SD.Failure,
                Message = "Role not found."
            });
        }

        var roleId = role.Id;

        var forms = await _context.RoleFormTemplates
            .Where(rft => rft.CompanyId == user.CompanyId && rft.RoleId == roleId)
            .Include(rft => rft.FormTemplate)
                .ThenInclude(ft => ft.FormFields)
            .Select(rft => rft.FormTemplate)
            .ToListAsync();

        var response = new ApiResponseDto<List<FormTemplateDto>>
        {
            Status = SD.Success,
            Message = "Forms retrieved successfully",
            Data = forms.Select(ft => ft.ToFormTemplateDto()).ToList()
        };

        return Ok(response);
    }

    [Authorize]
    [HttpPost("forms/{formTemplateId}/submit")]
    public async Task<IActionResult> SubmitForm(int formTemplateId, [FromBody] List<FromDataInputDto> formData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Status = SD.Failure,
                Message = "Invalid form data."
            });
        }

        var user = await _userManager.GetUserAsync(User);
        var formTemplate = await _context.FormTemplates.FindAsync(formTemplateId);

        if (formTemplate == null || formTemplate.CompanyId != user.CompanyId)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Status = SD.Failure,
                Message = "Form template not found or access denied."
            });
        }

        try
        {
            foreach (var fieldData in formData)
            {
                var employeeFormData = new EmployeeFormData
                {
                    UserId = user.Id,
                    FieldId = fieldData.FieldId,
                    FieldValue = fieldData.Value,
                    CompanyId = user.CompanyId
                };
                _context.EmployeeFormDatas.Add(employeeFormData);
            }

            await _context.SaveChangesAsync();

            return Ok(new ApiResponseDto<string>
            {
                Status = SD.Success,
                Message = "Form submitted successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Status = SD.Failure,
                Message = $"An error occurred: {ex.Message}"
            });
        }
    }

    [Authorize]
    [HttpGet("forms/employee-data/{userId}")]
    public async Task<IActionResult> GetEmployeeFormData(string userId)
    {
        var employeeFormData = await _context.EmployeeFormDatas
            .Where(e => e.UserId == userId)
            .Include(e => e.FormField)
            .Include(e => e.FormField.FormTemplate)
            .ToListAsync();

        if (employeeFormData == null || !employeeFormData.Any())
        {
            return NotFound(new ApiResponseDto<string>
            {
                Status = SD.Failure,
                Message = "No form data found for this employee."
            });
        }

        var groupedData = employeeFormData
            .GroupBy(e => e.FormField.FormTemplateId)
            .Select(g => new EmployeeFormDataResponseDto
            {
                FormTemplateId = g.Key,
                FormName = g.First().FormField.FormTemplate.FormName,
                FormData = g.Select(e => e.ToEmployeeFormDataDto()).ToList()
            })
            .ToList();

        var response = new ApiResponseDto<List<EmployeeFormDataResponseDto>>
        {
            Status = SD.Success,
            Message = "Employee form data retrieved successfully",
            Data = groupedData
        };

        return Ok(response);
    }
}