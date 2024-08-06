using CrmApiV2.Dtos.Project;
using CrmApiV2.Models;

namespace CrmApiV2.Mapper
{
    public static class ProjectMapper
    {
        public static ProjectDto ToProjectDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                WebsiteUrl = project.WebsiteUrl,
                ClientName = project.ClientName,
                StartDate = project.StartDate,
                MonthlyPrice = project.MonthlyPrice,
                CompanyId = project.CompanyId,
            };
        }

        public static Project ToProjectToCreateDto(this CreateProjectDto project)
        {
            return new Project
            {
                ProjectName = project.ProjectName,
                WebsiteUrl = project.WebsiteUrl,
                ClientName = project.ClientName,
                StartDate = project.StartDate,
                MonthlyPrice = project.MonthlyPrice,
               // CompanyId = user.CompanyId,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
            };
        }

        public static Project ToProjectToUpdateDto(this UpdateProjectDto project)
        {
            return new Project
            {
                ProjectName = project.ProjectName,
                WebsiteUrl = project.WebsiteUrl,
                ClientName = project.ClientName,
                StartDate = project.StartDate,
                MonthlyPrice = project.MonthlyPrice,
                ModifiedBy = "SuperAdmin",
                ModifiedOn = DateTime.Now,
                IsDeleted = false,
            };
        }
    }
}