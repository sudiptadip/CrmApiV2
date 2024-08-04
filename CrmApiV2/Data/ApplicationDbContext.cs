using CrmApiV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrmApiV2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
