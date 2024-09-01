using CrmApiV2.Models;
using CrmApiV2.Models.DynamicForm;
using CrmApiV2.Models.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CrmApiV2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserTimeLog> UserTimeLogs { get; set; }
        public DbSet<DailyUserSummary> DailyUserSummaries { get; set; }
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<RoleFormTemplate> RoleFormTemplates { get; set; }
        public DbSet<EmployeeFormData> EmployeeFormDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relationships and keys
            builder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.TimeLogs)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey(t => t.ApplicationUserId);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.DailySummaries)
                .WithOne(d => d.ApplicationUser)
                .HasForeignKey(d => d.ApplicationUserId);

            // Configuring composite keys and relationships
            builder.Entity<RoleFormTemplate>()
                .HasKey(rft => new { rft.RoleId, rft.FormTemplateId });

            builder.Entity<RoleFormTemplate>()
                .HasOne(rft => rft.Role)
                .WithMany(r => r.RoleFormTemplates)
                .HasForeignKey(rft => rft.RoleId);

            builder.Entity<RoleFormTemplate>()
                .HasOne(rft => rft.FormTemplate)
                .WithMany(ft => ft.RoleFormTemplates)
                .HasForeignKey(rft => rft.FormTemplateId);

            builder.Entity<EmployeeFormData>()
                .HasOne(efd => efd.User)
                .WithMany(u => u.EmployeeFormDatas)
                .HasForeignKey(efd => efd.UserId);

            builder.Entity<EmployeeFormData>()
                .HasOne(efd => efd.FormField)
                .WithMany()
                .HasForeignKey(efd => efd.FieldId);
        }

    }
}