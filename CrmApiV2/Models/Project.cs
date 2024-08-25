using CrmApiV2.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace CrmApiV2.Models
{
    public class Project : CommonItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Url]
        public string WebsiteUrl { get; set; }

        [Required]
        public string ClientName { get; set; }

        public DateTime StartDate { get; set; }

        public double MonthlyPrice { get; set; }

        public bool IsComplete { get; set; } = false;

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
