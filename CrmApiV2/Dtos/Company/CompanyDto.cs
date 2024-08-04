using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Company
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
