using CrmApiV2.Dtos.Account;

namespace CrmApiV2.Dtos.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime StartDate { get; set; }
        public double MonthlyPrice { get; set; }
        public int CompanyId { get; set; }
        public List<UserDto> AssignedEmployees { get; set; }
    }
}
