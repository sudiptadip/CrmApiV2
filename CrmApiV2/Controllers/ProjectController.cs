using CrmApiV2.Dtos.Company;
using CrmApiV2.Dtos.Project;
using CrmApiV2.Dtos.Response;
using CrmApiV2.Interface;
using CrmApiV2.Mapper;
using CrmApiV2.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrmApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;
        public ProjectController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var projectList = await _projectRepo.GetAllAsync();

            return Ok(new ApiResponseDto<List<ProjectDto>>
            {
                Status = SD.Success,
                Message = "Successfully Fetch Data",
                Data = projectList
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = "Invalid data"
                });
            }

            try
            {
               var project = await _projectRepo.CreateAsync(createProjectDto);

                var response = new ApiResponseDto<ProjectDto>
                {
                    Status = SD.Success,
                    Message = "Project created successfully",
                    Data = project.ToProjectDto()
                };

                return Ok(response);
                
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = ex.Message
                });
            }
        }


        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var project = await _projectRepo.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound(new ApiResponseDto<ProjectDto>
                {
                    Status = SD.Failure,
                    Message = "Project not found"
                });
            }

            return Ok(new ApiResponseDto<ProjectDto>
            {
                Status = SD.Success,
                Message = "Successfully fetched project data",
                Data = project
            });
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto<ProjectDto>
                {
                    Status = SD.Failure,
                    Message = "Invalid data"
                });
            }

            try
            {
                var updatedProject = await _projectRepo.UpdateAsync(updateProjectDto);

                if (updatedProject == null)
                {
                    return NotFound(new ApiResponseDto<ProjectDto>
                    {
                        Status = SD.Failure,
                        Message = "Project not found"
                    });
                }

                return Ok(new ApiResponseDto<ProjectDto>
                {
                    Status = SD.Success,
                    Message = "Project updated successfully",
                    Data = updatedProject.ToProjectDto()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<ProjectDto>
                {
                    Status = SD.Failure,
                    Message = ex.Message
                });
            }
        }


        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProject = await _projectRepo.DeleteAsync(id);

            if (deletedProject == null)
            {
                return NotFound(new ApiResponseDto<ProjectDto>
                {
                    Status = SD.Failure,
                    Message = "Project not found"
                });
            }

            return Ok(new ApiResponseDto<ProjectDto>
            {
                Status = SD.Success,
                Message = "Project deleted successfully",
                Data = deletedProject.ToProjectDto()
            });
        }


    }
}
