using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Account
{
    public class VerifyOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Otp { get; set; }
    }
}
