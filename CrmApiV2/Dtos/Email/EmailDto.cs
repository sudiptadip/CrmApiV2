using System.ComponentModel.DataAnnotations;

namespace CrmApiV2.Dtos.Email
{
    public class EmailDto
    {
        [Required]
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
