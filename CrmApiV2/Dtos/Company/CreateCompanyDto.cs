using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Company
{
    public class UpdateCompanyDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}