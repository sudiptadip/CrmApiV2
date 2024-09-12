using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Account
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
