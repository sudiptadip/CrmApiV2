using CrmApiV2.Data;
using CrmApiV2.Dtos.Project;
using CrmApiV2.Interface;
using CrmApiV2.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using CrmApiV2.Dtos.Account;

namespace CrmApiV2.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;
        public ProjectRepository(ApplicationDbContext db, ICurrentUserService currentUserService)
        {
            _db = db;
            _currentUserService = currentUserService;
        }

        public async Task<Project> CreateAsync(CreateProjectDto project)
        {
            var user = _currentUserService.GetCurrentUser();
            var existingProject = await _db.Projects.FirstOrDefaultAsync(p => p.ProjectName == project.ProjectName && !p.IsDeleted && p.CompanyId == user.CompanyId);

            if (existingProject != null)
            {
                throw new InvalidOperationException("Project Name Already Exists");
            }

            var projectModel = new Project
            {
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                WebsiteUrl = project.WebsiteUrl,
                StartDate = project.StartDate,
                MonthlyPrice = project.MonthlyPrice,
                CompanyId = user.CompanyId,
                IsComplete = false,
                IsDeleted = false,
                CreatedBy = user.Id,
                CreatedOn = DateTime.Now
            };

            await _db.Projects.AddAsync(projectModel);

            await _db.SaveChangesAsync();

            List<UserProject> userProject = new List<UserProject>();

            foreach (var item in project.AssignedUserIds)
            {
                userProject.Add(new UserProject { ProjectId = projectModel.Id, UserId = item });
            }

            await _db.UserProjects.AddRangeAsync(userProject);
            await _db.SaveChangesAsync();

            return projectModel;
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var user = _currentUserService.GetCurrentUser();

            return await _db.Projects
                .Where(p => !p.IsDeleted && p.CompanyId == user.CompanyId)
                .Include(p => p.UserProjects)
                .ThenInclude(up => up.User)
                .Select(p => new ProjectDto 
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    ClientName = p.ClientName,
                    WebsiteUrl = p.WebsiteUrl,
                    StartDate = p.StartDate,
                    MonthlyPrice = p.MonthlyPrice,
                    CompanyId = p.CompanyId,
                    AssignedEmployees = p.UserProjects
                        .Select(up => new UserDto
                        {
                            Id = up.User.Id,
                            UserName = up.User.UserName,
                            Email = up.User.Email,
                            Name = up.User.Name,
                            Address = up.User.Address
                        }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Project?> UpdateAsync(UpdateProjectDto project)
        {
            var user = _currentUserService.GetCurrentUser();
            var existingProject = await _db.Projects
                .Include(p => p.UserProjects)
                .FirstOrDefaultAsync(p => p.ProjectName == project.ProjectName && !p.IsDeleted && p.CompanyId == user.CompanyId);

            if (existingProject == null)
            {
                return null;
            }

            existingProject.ProjectName = project.ProjectName;
            existingProject.ClientName = project.ClientName;
            existingProject.WebsiteUrl = project.WebsiteUrl;
            existingProject.StartDate = project.StartDate;
            existingProject.MonthlyPrice = project.MonthlyPrice;
            existingProject.ModifiedBy = user.Id;
            existingProject.ModifiedOn = DateTime.Now;

            // Update UserProjects
            var currentAssignedEmployees = existingProject.UserProjects.Select(up => up.UserId).ToList();
            var newAssignedEmployees = project.AssignedUserIds;

            // Find employees to remove
            var employeesToRemove = currentAssignedEmployees.Except(newAssignedEmployees).ToList();
            foreach (var userId in employeesToRemove)
            {
                var userProject = existingProject.UserProjects.FirstOrDefault(up => up.UserId == userId);
                if (userProject != null)
                {
                    _db.UserProjects.Remove(userProject);
                }
            }

            // Find employees to add
            var employeesToAdd = newAssignedEmployees.Except(currentAssignedEmployees).ToList();
            foreach (var userId in employeesToAdd)
            {
                existingProject.UserProjects.Add(new UserProject { ProjectId = existingProject.Id, UserId = userId });
            }

            await _db.SaveChangesAsync();

            return existingProject;
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var user = _currentUserService.GetCurrentUser();

            var project = await _db.Projects
                .Where(p => !p.IsDeleted && p.CompanyId == user.CompanyId && p.Id == id)
                .Include(p => p.UserProjects)
                .ThenInclude(up => up.User)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return null;
            }

            return new ProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                WebsiteUrl = project.WebsiteUrl,
                StartDate = project.StartDate,
                MonthlyPrice = project.MonthlyPrice,
                CompanyId = project.CompanyId,
                AssignedEmployees = project.UserProjects
                    .Select(up => new UserDto
                    {
                        Id = up.User.Id,
                        UserName = up.User.UserName,
                        Email = up.User.Email,
                        Name = up.User.Name,
                        Address = up.User.Address
                    }).ToList()
            };
        }

        public async Task<Project?> DeleteAsync(int id)
        {
            var user = _currentUserService.GetCurrentUser();

            var project = await _db.Projects
                .Where(p => !p.IsDeleted && p.CompanyId == user.CompanyId && p.Id == id)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return null;
            }

            project.IsDeleted = true;
            project.ModifiedBy = user.Id;
            project.ModifiedOn = DateTime.Now;

            await _db.SaveChangesAsync();

            return project;
        }

    }
}
