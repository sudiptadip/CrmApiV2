using CrmApiV2.Models;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

            List<IdentityRole> roles = new List<IdentityRole> {
                new IdentityRole {
                    Name = "Super Admin",
                    NormalizedName = "SUPER_ADMIN"
                },
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole {
                    Name = "Seo",
                    NormalizedName = "SEO"
                },
                new IdentityRole {
                    Name = "Developer",
                    NormalizedName = "DEVELOPER"
                },
                new IdentityRole {
                    Name = "Sales",
                    NormalizedName = "SALES"
                },
                new IdentityRole {
                    Name = "Content Writer",
                    NormalizedName = "CONTENT_WRITER"
                },
                new IdentityRole {
                    Name = "Academic Writer",
                    NormalizedName = "ACADEMIC_WRITER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}