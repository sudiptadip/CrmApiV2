using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string Role { get; set; }
        public string? Address { get; set; }
        [Required]
        public int ComapnyId { get; set; }
    }
}