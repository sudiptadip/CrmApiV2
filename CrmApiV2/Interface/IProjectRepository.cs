using CrmApiV2.Dtos.Project;
using CrmApiV2.Models;

namespace CrmApiV2.Interface
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(CreateProjectDto project);
        Task<List<ProjectDto>> GetAllAsync();
        Task<Project?> UpdateAsync(UpdateProjectDto project);       
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<Project?> DeleteAsync(int id);
    }
}
