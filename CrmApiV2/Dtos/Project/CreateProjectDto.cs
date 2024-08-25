using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Project
{
    public class CreateProjectDto
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public double MonthlyPrice { get; set; }
        [Required]
        public List<string> AssignedUserIds { get; set; } = new List<string>();
    }
}