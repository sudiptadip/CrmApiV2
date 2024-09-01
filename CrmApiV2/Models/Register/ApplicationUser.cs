using CrmApiV2.Models.Common;
using CrmApiV2.Models.DynamicForm;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmApiV2.Models.Register
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
        public ICollection<UserTimeLog> TimeLogs { get; set; }
        public ICollection<DailyUserSummary> DailySummaries { get; set; }
        public ICollection<EmployeeFormData> EmployeeFormDatas { get; set; }
    }
}